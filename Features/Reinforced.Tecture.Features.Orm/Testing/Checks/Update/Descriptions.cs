﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Delete;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    sealed class UpdateCheckDescription : CheckDescription<Commands.Update.Update>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => UpdateChecks.Update<object>(a.Assertions(c.Entity), c.Annotation));

        protected override Type[] GetTypeArguments(Commands.Update.Update command)
        {
            return new Type[] { command.EntityType };
        }
    }

    public static class Descriptions
    {
        public static void Basic(this ChecksConfigurator<Commands.Update.Update> c) => c.Enlist(new UpdateCheckDescription());
    }
}
