using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class SimpleSelectAnswer : Answer
    {
        public SimpleSelectAnswer()
            : base()
        { }

        public SimpleSelectAnswer(string text, string userName, Question question, bool isCorrect)
            : this()
        {
            ThrowIfTextInvalid(text);
            ThrowIfUserNameInvalid(userName);
            ThrowIfQuestionInvalid(question);

            Text = text;
            Question = question;
            IsCorrect = isCorrect;
            MarkAsModified(userName);
        }
    }
}
