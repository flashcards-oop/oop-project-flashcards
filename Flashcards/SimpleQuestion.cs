using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    public class SimpleQuestion : Question
    {
        public string Definition { get; }

        public SimpleQuestion(string definition)
        {
            Definition = definition;
        }
    }
}
