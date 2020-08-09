﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Testing.Generation;
using Reinforced.Tecture.Testing.Stories;

namespace Reinforced.Tecture.Testing.Checks
{
    public static class Descriptions
    {
        public static void Basic(this ChecksConfigurator<CommandBase> c) => c.Add(new AnnotationCheckDescription());
        public static void Basic(this ChecksConfigurator<Comment> c) => c.Add(new CommentCheckDescription());
        public static void Basic(this ChecksConfigurator<Save> c) => c.Add(new SaveCheckDescription());

        public static void Basics(this TestGenerator tg)
        {
            tg.For<CommandBase>().Basic();
            tg.For<Comment>().Basic();
            tg.For<Save>().Basic();
        }
    }

    sealed class AnnotationCheckDescription : CheckDescription<CommandBase>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Annotated(null));

        protected override object[] GetArguments(CommandBase command)
        {
            return new[] { command.Annotation };
        }
    }

    sealed class CommentCheckDescription : CheckDescription<Comment>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Comment(null));

        protected override object[] GetArguments(Comment command)
        {
            return new[] { command.Annotation };
        }
    }

    sealed class SaveCheckDescription : CheckDescription<Save>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Saved());
    }

}