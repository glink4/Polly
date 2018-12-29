﻿using System;
using System.Threading;

namespace Polly.Specs.Helpers.Custom.PreExecute
{
    internal class PreExecutePolicy : Policy
    {
        private Action _preExecute;

        public static PreExecutePolicy Create(Action preExecute)
        {
            return new PreExecutePolicy(preExecute);
        }

        internal PreExecutePolicy(Action preExecute)
            :base(ExceptionPredicates.None)
        {
            _preExecute = preExecute ?? throw new ArgumentNullException(nameof(preExecute));
        }

        protected override TResult Implementation<TResult>(Func<Context, CancellationToken, TResult> action, Context context, System.Threading.CancellationToken cancellationToken)
        {
            return PreExecuteEngine.Implementation(_preExecute, action, context, cancellationToken);
        }
    }

    internal class PreExecutePolicy<TResult> : Policy<TResult>
    {
        private Action _preExecute;

        public static PreExecutePolicy<TResult> Create(Action preExecute)
        {
            return new PreExecutePolicy<TResult>(preExecute);
        }

        internal PreExecutePolicy(Action preExecute)
            : base(ExceptionPredicates.None, ResultPredicates<TResult>.None)
        {
            _preExecute = preExecute ?? throw new ArgumentNullException(nameof(preExecute));
        }

        protected override TResult Implementation(Func<Context, CancellationToken, TResult> action, Context context, System.Threading.CancellationToken cancellationToken)
        {
            return PreExecuteEngine.Implementation(_preExecute, action, context, cancellationToken);
        }
    }
}