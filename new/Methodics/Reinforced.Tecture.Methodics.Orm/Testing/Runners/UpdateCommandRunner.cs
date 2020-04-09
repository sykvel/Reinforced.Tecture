﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Runners
{
    public class UpdateAssumptionArgument<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UpdateAssumptionArgument(T entity, Update sideEffect, ICollectionProvider collectionProvider)
        {
            Entity = entity;
            SideEffect = sideEffect;
            CollectionProvider = collectionProvider;
        }

        public T Entity { get; private set; }

        public Update SideEffect { get; private set; }

        public ICollectionProvider CollectionProvider { get; private set; }
    }
    class UpdateCommandRunner : ICommandRunner<Update>
    {
        private readonly ICollectionProvider _env;
        private readonly Dictionary<Type, List<Delegate>> _assumedActions = new Dictionary<Type, List<Delegate>>();

        public UpdateCommandRunner(ICollectionProvider env)
        {
            _env = env;
        }

        public UpdateCommandRunner Assume<T>(Action<UpdateAssumptionArgument<T>> ua)
        {
            if (!_assumedActions.ContainsKey(typeof(T))) _assumedActions[typeof(T)] = new List<Delegate>();
            _assumedActions[typeof(T)].Add(ua);
            return this;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        public void Run(Update cmd)
        {
            var coll = _env.GetCollection(cmd.EntityType);
            coll.Remove(cmd.Entity);
            if (_assumedActions.ContainsKey(cmd.EntityType))
            {
                var l = _assumedActions[cmd.EntityType];
                var inst = Activator.CreateInstance(typeof(UpdateAssumptionArgument<>).MakeGenericType(cmd.EntityType), new[] { cmd.Entity, cmd, _env });
                foreach (var del in l)
                {
                    del.DynamicInvoke(inst);
                }
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(Update cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
