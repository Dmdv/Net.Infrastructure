﻿using System;
using Common.Annotations;

// ReSharper disable CodeCleanup
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedParameter.Global

namespace Net.Common.Extensions
{
	public static class NullExtensions
	{
		[PublicAPI]
		public static TResult IfNotNull<TInstance, TResult>(
			[CanBeNull] this TInstance instance,
			[NotNull] Func<TInstance, TResult> functor,
			TResult defaultValue = default(TResult)) where TInstance : class
		{
			return instance == default(TInstance) ? defaultValue : functor(instance);
		}

		[PublicAPI]
		public static TInstance IfNotNull<TInstance>(
			[CanBeNull] this TInstance instance,
			[NotNull] Action<TInstance> functor) where TInstance : class
		{
			if (instance != default (TInstance))
			{
				functor(instance);
			}

			return instance;
		}

		[PublicAPI]
		public static TResult IfHasValue<TInstance, TResult>(
			[CanBeNull] this TInstance? instance,
			[NotNull] Func<TInstance?, TResult> functor,
			TResult defaultValue = default(TResult)) where TInstance : struct
		{
			return instance == null ? defaultValue : functor(instance);
		}

		[PublicAPI]
		public static TInstance? IfHasValue<TInstance>(
			[CanBeNull] this TInstance? instance,
			[NotNull] Action<TInstance> functor) where TInstance : struct
		{
			if (instance.HasValue)
			{
				functor(instance.Value);
			}

			return instance;
		}

		[PublicAPI]
		public static TInstance IfNull<TInstance>(
			[CanBeNull] this TInstance instance,
			TInstance defaultValue = default(TInstance)) where TInstance : class
		{
			return instance ?? defaultValue;
		}

		[PublicAPI]
		public static TInstance IfNull<TInstance>(
			[CanBeNull] this TInstance? instance,
			TInstance defaultValue = default(TInstance)) where TInstance : struct
		{
			return instance.HasValue ? instance.Value : defaultValue;
		}

		[PublicAPI]
		public static TInstance ThrowIf<TInstance>(
			[CanBeNull] this TInstance instance,
			Func<TInstance, bool> condition,
			Exception exception) where TInstance : class
		{
			if ((instance != default(TInstance)) && condition(instance))
			{
				throw exception;
			}

			return instance;
		}

		[NotNull]
		[PublicAPI]
		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance instance,
			Exception nullException) where TInstance : class
		{
			if (instance == default(TInstance))
			{
				throw nullException;
			}

			return instance;
		}

		[NotNull]
		[PublicAPI]
		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance instance,
			Func<Exception> nullExceptionGetter) where TInstance : class
		{
			if (instance == default(TInstance))
			{
				throw nullExceptionGetter();
			}

			return instance;
		}

		[PublicAPI]
		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance? instance,
			Func<Exception> nullExceptionGetter) where TInstance : struct
		{
			if (instance == null)
			{
				throw nullExceptionGetter();
			}

			return instance.Value;
		}

		[PublicAPI]
		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance instance,
			Action exceptionAction) where TInstance : class
		{
			if (instance == default(TInstance))
			{
				exceptionAction.Invoke();
			}

			return instance;
		}

		[NotNull]
		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance instance,
			string message) where TInstance : class
		{
			return ThrowIfNull(instance, new NullReferenceException(message));
		}

		public static TInstance ThrowIfNull<TInstance>(
			[CanBeNull] this TInstance? instance,
			string message) where TInstance : struct
		{
			return ThrowIfNull(instance, () => new NullReferenceException(message));
		}
	}
}

//public static void Do<TInstance>([CanBeNull] this TInstance instance, [NotNull] Action<TInstance> action) where TInstance : class
//{
//	if (instance == null)
//	{
//		return;
//	}

//	action(instance);
//}