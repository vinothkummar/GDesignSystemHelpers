using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ganss.XSS;
using GDSHelpers.Extensions;
using GDSHelpers.Models.FormSchema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace GDSHelpers
{
    public interface IGdsValidation
    {
        /// <summary>
        /// Validates a PageVM object against the Request.Form object after postback
        /// </summary>
        /// <param name="pageVm">A PageVM object containing the current page.</param>
        /// <param name="requestForm">The Request.Form object after postback of the current page.</param>
        /// <param name="stripHtml">Do you want to remove HTML from the answer (true / false)</param>
        /// <param name="restrictedWords">A list of restricted words i.e. new List<string> { "javascript", "onclick" } </param>
        /// <param name="allowedChars">A hashset of whitelisted chars i.e. new HashSet<char>(@"abcdefg");</param>
        /// <returns>A validated PageVM object with error flags and messages marked against each question.</returns>
        PageVM ValidatePage(PageVM pageVm, IFormCollection requestForm,
            bool stripHtml = false, List<string> restrictedWords = null, HashSet<char> allowedChars = null);

        /// <summary>
        /// Cleans any text of dangerous input, i.e.<script></script> or onclick="".
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="stripHtml">Do you want to remove HTML from the answer (true / false)</param>
        /// <param name="restrictedWords">A list of restricted words i.e. new List<string> { "javascript", "onclick" } </param>
        /// <param name="allowedChars">A hashset of whitelisted chars i.e. new HashSet<char>(@"abcdefg");</param>
        /// <returns>Returns a cleaned string or empty string if dangerous input found.</returns>
        string CleanText(string answer, bool stripHtml = false, List<string> restrictedWords = null, HashSet<char> allowedChars = null);

        /// <summary>
        /// Returns a count of the number of words in a string
        /// </summary>
        /// <param name="text">The string to word count</param>
        /// <returns>Returns an int value of the number of words</returns>
        int WordCount(string text);

    }



    public class GdsValidation : IGdsValidation
    {
        public PageVM ValidatePage(PageVM pageVm, IFormCollection requestForm, 
            bool stripHtml = false, List<string> restrictedWords = null, HashSet<char> allowedChars = null)
        {
            foreach (var question in pageVm.Questions)
            {
                //Get the answer
                var answer = CleanText(requestForm[question.QuestionId].ToString(), stripHtml, restrictedWords, allowedChars);
                question.Answer = answer;

                //Set the next page id if our answer matches a rule
                var nextPageId = question.AnswerLogic?.FirstOrDefault(m => m.Value == answer)?.NextPageId;
                if (nextPageId != null) pageVm.NextPageId = nextPageId;

                //For dynamic questions:
                if (question.ShowWhen != null)
                {
                    //Set variables relating to dynamic questions
                    var parentId = question.ShowWhen.QuestionId;
                    var parentAnswer = CleanText(requestForm[parentId].ToString(), stripHtml, restrictedWords, allowedChars);
                    var parentAnswerRequired = question.ShowWhen.Answer;
                    var shownToUser = parentAnswer == parentAnswerRequired;

                    //Delete contents if parent answer is not the correct one
                    if (shownToUser)
                    {
                        //Check whether the question is required
                        if (question.Validation?.Required.IsRequired == true && string.IsNullOrEmpty(answer))
                        {
                            question.Validation.IsErrored = true;
                            question.Validation.ErrorMessage = question.Validation.Required.ErrorMessage;
                        }
                    }
                    else
                    {
                        question.Answer = string.Empty;
                    }
                }
                else
                {
                    //Check whether the question is required
                    if (question.Validation?.Required.IsRequired == true && string.IsNullOrEmpty(answer))
                    {
                        question.Validation.IsErrored = true;
                        question.Validation.ErrorMessage = question.Validation.Required.ErrorMessage;
                    }
                }

                if (!question.Validation?.IsErrored == true && (!string.IsNullOrEmpty(answer)))
                {
                    //Check length
                    var lengthType = question.Validation?.MaxLength?.Type.ToLower();
                    var answerLength = lengthType == "words" ? WordCount(answer) : answer.Replace("\r\n", "\n").Length;

                    if (question.Validation?.MinLength?.Min > answerLength)
                    {
                        question.Validation.IsErrored = true;
                        question.Validation.ErrorMessage = question.Validation.MinLength.ErrorMessage;
                    }

                    if (question.Validation?.MaxLength?.Max < answerLength)
                    {
                        question.Validation.IsErrored = true;
                        question.Validation.ErrorMessage = question.Validation.MaxLength.ErrorMessage;
                    }
                }

                if (!question.Validation?.IsErrored == true && (!string.IsNullOrEmpty(answer)) && (question.Validation?.Regex != null))
                {
                    //check regex
                    var error = false;
                    var regEx = question.Validation.Regex;
                    var regexType = regEx.Regex;
                    switch (regexType)
                    {
                        case "name":
                            error = !RegexUtilities.IsValidName(answer);
                            break;
                        case "phone":
                            error = !RegexUtilities.IsValidPhoneNumber(answer);
                            break;
                        case "email":
                            error = !RegexUtilities.IsValidEmail(answer);
                            break;
                        case "freetext":
                            error = !RegexUtilities.IsValidFreeText(answer);
                            break;
                        case "custom":
                            error = !RegexUtilities.IsValidCustomText(regEx?.Expression, answer);
                            break;
                    }

                    if (error)
                    {
                        question.Validation.IsErrored = true;
                        question.Validation.ErrorMessage = question.Validation.Regex.ErrorMessage;
                    }
                }

                //Check if any other questions relevant to this are answered
                if ((!question.Validation?.IsErrored == true) && question.Validation?.RequiredIf != null)
                {
                    //check parent question is answered
                    var parentAnswer = CleanText(requestForm[question.Validation.RequiredIf.ParentId].ToString(), stripHtml, restrictedWords, allowedChars);
                    if (!string.IsNullOrEmpty(parentAnswer))
                    {
                        var errored = string.IsNullOrEmpty(answer);//check if this question's been answered
                        if (!string.IsNullOrEmpty(question.Validation.RequiredIf.LinkedIds) && (question.Validation.RequiredIf.InclusiveLogic.ToLower() == "or") && errored)
                        {
                            //check if other linked questions have been answered
                            errored = (!CheckRelatedQuestions(requestForm, question.Validation.RequiredIf.LinkedIds, stripHtml, restrictedWords, allowedChars));
                        }

                        if (errored)
                        {
                            question.Validation.IsErrored = true;
                            question.Validation.ErrorMessage = question.Validation.RequiredIf.ErrorMessage;
                        }
                    }
                }

                //Check Minimum\Maximum Selected
                var selectedOptionsCount = answer.Split(',').Length;
                var min = question.Validation?.Selected?.Min;
                var max = question.Validation?.Selected?.Max;
                if (selectedOptionsCount < min || selectedOptionsCount > max)
                {
                    question.Validation.IsErrored = true;
                    question.Validation.ErrorMessage = question.Validation.Selected.ErrorMessage;
                }
            }

            return pageVm;
        }

        /// <summary>
        /// checks if other linked questions have been answered
        /// </summary>
        /// <param name="requestForm"></param>
        /// <param name="questionIdList">comma separated list of question ids</param>
        /// <param name="stripHtml"></param>
        /// <param name="restrictedWords"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public bool CheckRelatedQuestions(IFormCollection requestForm, string questionIdList, bool stripHtml, List<string> restrictedWords, HashSet<char> allowedChars)
        {
            var questionIds = questionIdList.Split(',').ToList();
            var answerCount = 0;
            foreach (var questionId in questionIds)
            {
                var linkedAnswer = CleanText(requestForm[questionId].ToString(), stripHtml, restrictedWords, allowedChars);
                if (!string.IsNullOrEmpty(linkedAnswer))
                {
                    answerCount++;
                    break;
                }
            }
            return answerCount > 0;
        }

        public string CleanText(string answer, bool stripHtml = false, 
            List<string>restrictedWords = null, HashSet<char> allowedChars = null)
        {
            if (string.IsNullOrEmpty(answer))
                return string.Empty;

            //Strip out any Html
            if (stripHtml) answer = answer.StripHtml();

            //Replace any Smart Quotes
            answer = answer.ReplaceSmartQuotes();

            //Check for non-allowed words
            if (restrictedWords != null)
            {
                foreach (var word in restrictedWords)
                {
                    if (answer.ToLower().Contains(word)) return "";
                }
            }

            //Check for non-allowed chars
            if (allowedChars != null)
            {
                answer = string.Concat(answer.Where(c => allowedChars.Contains(c)));
            }

            //check for ampersand
            answer = answer.Replace("&amp;", "&");
            answer = answer.Replace("amp;", "&");

            //Remove leading and trailing spaces
            answer = answer.Trim();

            return answer;
        }
        
        public int WordCount(string text)
        {
            var regex = new Regex(@"\S+", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);
            var count = matches.Count;
            return count;
        }
    }
}
