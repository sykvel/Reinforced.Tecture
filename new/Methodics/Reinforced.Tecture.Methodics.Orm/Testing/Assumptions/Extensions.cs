﻿using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Testing.Assumptions;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Assumptions
{
    public static class Extensions
    {
        public static Assuming Orm(this Assuming a, Action<OrmAssuming> ormAssumptions)
        {
            OrmAssuming om = new OrmAssuming(a);
            if (ormAssumptions != null) ormAssumptions(om);
            return a;
        }

        public static OrmAssuming Assume<TCommand,TEntity>(this OrmAssuming am, Func<TEntity, bool> predicate,
            Action<TEntity, ICollectionProvider> action) where TCommand : CommandBase, IOrmCommand
        {
            am._orig.Assume(new OrmAssumption<TCommand>(typeof(TEntity), predicate, action, false));
            return am;
        }

        public static OrmAssuming Assume<TCommand, TEntity>(this OrmAssuming am, Func<TEntity, bool> predicate,
            Action<TEntity> action) where TCommand : CommandBase, IOrmCommand
        {
            am._orig.Assume(new OrmAssumption<TCommand>(typeof(TEntity), predicate, action, true));
            return am;
        }

        public static OrmAssuming Assume<TCommand, TEntity>(this OrmAssuming am,
            Action<TEntity, ICollectionProvider> action) where TCommand : CommandBase, IOrmCommand
        {
            am._orig.Assume(new OrmAssumption<TCommand>(typeof(TEntity), null, action, false));
            return am;
        }

        public static OrmAssuming Assume<TCommand, TEntity>(this OrmAssuming am,
            Action<TEntity> action) where TCommand : CommandBase, IOrmCommand
        {
            am._orig.Assume(new OrmAssumption<TCommand>(typeof(TEntity), null, action, true));
            return am;
        }

    }
}
