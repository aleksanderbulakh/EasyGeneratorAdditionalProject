using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class QuestionAnswer : Answers
    {
        public QuestionAnswer()
            : base()
        { }

        public QuestionAnswer(string text, string userName, Question question)
            : this()
        {
            ThrowIfTextInvalid(text);
            ThrowIfUserNameInvalid(userName);
            ThrowIfQuestionInvalid(question);

            Text = text;
            Question = question;
            MarkAsModified(userName);
        }

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
    }
}
