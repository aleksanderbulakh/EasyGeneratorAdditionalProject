using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Answers : Identifier
    {
        public virtual Question Question { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answers() 
            : base() { }

        public void UpdateText(string text, string userName)
        {
            ThrowIfTextInvalid(text);
            ThrowIfUserNameInvalid(userName);

            Text = text;
            MarkAsModified(userName);
        }

        public void UpdateState(bool isCorrect, string userName)
        {
            ThrowIfUserNameInvalid(userName);

            IsCorrect = isCorrect;
            MarkAsModified(userName);
        }

        protected void ThrowIfTextInvalid(string text)
        {
            if (text == null || text.Length == 0 || text.Length > 255)
                throw new ArgumentException("Invalid text");
        }

        protected void ThrowIfUserNameInvalid(string userName)
        {
            if (userName == null || userName.Length == 0)
                throw new ArgumentException("Invalid userName");
        }

        protected void ThrowIfQuestionInvalid(Question question)
        {
            if (question == null)
                throw new ArgumentException("Invalid question");
        }

        protected void MarkAsModified(string userName)
        {
            Question.ModifiedBy = userName;
            Question.MarkAsModified();
        }
    }
}
