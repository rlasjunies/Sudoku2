using Fluxor.Extensions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Fluxor.DependencyInjection
{
	internal class DiscoveredEffectWithStateMethod
	{
		public readonly Type HostClassType;
		public readonly MethodInfo MethodInfo;
		public readonly Type ActionType;
		public readonly Type StateType;
		public readonly bool RequiresActionParameterInMethod;

		public DiscoveredEffectWithStateMethod(
			Type hostClassType,
			EffectWithStateMethodAttribute attribute,
			MethodInfo methodInfo)
		{
			ParameterInfo[] methodParameters = methodInfo.GetParameters();
			if (attribute.ActionType == null && methodParameters.Length != 3)
				throw new ArgumentException(
					$"Method must have 3 parameters (action, state, IDispatcher)"
						+ $" when [{nameof(EffectMethodAttribute)}] has no {nameof(EffectMethodAttribute.ActionType)} specified. "
						+ methodInfo.GetClassNameAndMethodName(),
					nameof(MethodInfo));

			if (attribute.ActionType != null && methodParameters.Length != 1)
				throw new ArgumentException(
					$"Method must have 1 parameter (IDispatcher)"
						+ $" when [{nameof(EffectMethodAttribute)}] has an {nameof(EffectMethodAttribute.ActionType)} specified. "
						+ methodInfo.GetClassNameAndMethodName(),
					nameof(methodInfo));

			// TODO May need to add a control for the 2nd parameter taht should be IState

			//if ( attribute.StateType == null)
			//	throw new ArgumentException(
			//		$"Method must have State parameters (action, state, IDispatcher)"
			//			+ $" when [{nameof(EffectMethodAttribute)}] has no {nameof(EffectMethodAttribute.ActionType)} specified. "
			//			+ methodInfo.GetClassNameAndMethodName(),
			//		nameof(MethodInfo));

			Type lastParameterType = methodParameters[methodParameters.Length - 1].ParameterType;
			if (lastParameterType != typeof(IDispatcher))
				throw new ArgumentException(
					$"The last parameter of a method should be an {nameof(IDispatcher)}"
						+ $" when decorated with an [{nameof(EffectMethodAttribute)}]. "
						+ methodInfo.GetClassNameAndMethodName(),
					nameof(methodInfo));

			if (methodInfo.ReturnType != typeof(Task))
				throw new ArgumentException(
					$"Effect methods must have a return type of {nameof(Task)}. " + methodInfo.GetClassNameAndMethodName(),
					nameof(methodInfo));

			HostClassType = hostClassType;
			MethodInfo = methodInfo;
			ActionType = attribute.ActionType ?? methodParameters[0].ParameterType;
			StateType = attribute.StateType ?? methodParameters[1].ParameterType;
			RequiresActionParameterInMethod = attribute.ActionType == null;
		}
	}
}
