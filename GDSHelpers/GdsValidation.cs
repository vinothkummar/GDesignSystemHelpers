using System.Collections.Generic;
using System.Linq;
using Ganss.XSS;
using GDSHelpers.Models.FormSchema;
using Microsoft.AspNetCore.Http;

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


                //Check if question is required
                if (question.Validation?.Required.IsRequired == true && string.IsNullOrEmpty(answer))
                {
                    question.Validation.IsErrored = true;
                    question.Validation.ErrorMessage = question.Validation.Required.ErrorMessage;
                }


                //Check length
                if (question.Validation?.MinLength?.Min > answer.Length)
                {
                    question.Validation.IsErrored = true;
                    question.Validation.ErrorMessage = question.Validation.MinLength.ErrorMessage;
                }

                if (question.Validation?.MaxLength?.Max < answer.Length)
                {
                    question.Validation.IsErrored = true;
                    question.Validation.ErrorMessage = question.Validation.MaxLength.ErrorMessage;
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

        
        public string CleanText(string answer, bool stripHtml = false, 
            List<string>restrictedWords = null, HashSet<char> allowedChars = null)
        {
            //Strip out any Html
            if (stripHtml)
            {
                var htmlSanitizer = new HtmlSanitizer();
                answer = htmlSanitizer.Sanitize(answer);
            }

            //Check for non-allowed words
            if (restrictedWords != null)
            {
                foreach (var word in restrictedWords)
                {
                    if (answer.Contains(word)) return "";
                }
            }

            //Check for non-allowed chars
            if (allowedChars != null)
            {
                answer = string.Concat(answer.Where(c => allowedChars.Contains(c)));
            }

            return answer;
        }

    }
}
