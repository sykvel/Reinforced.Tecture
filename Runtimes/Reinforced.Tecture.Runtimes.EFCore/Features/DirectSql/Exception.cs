﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql
{
    public class EfCoreDirectSqlException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public EfCoreDirectSqlException(string message) : base(message)
        {
        }
    }
}
