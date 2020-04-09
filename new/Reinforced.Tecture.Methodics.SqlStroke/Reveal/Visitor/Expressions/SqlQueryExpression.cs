﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    abstract class SqlQueryExpression
    {
        public bool IsTop { get; set; }
        public abstract string Serialize(List<Expression> sqlParams);
    }
}
