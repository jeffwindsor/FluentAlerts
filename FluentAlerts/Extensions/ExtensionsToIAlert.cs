using System;
using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlert
    {
        public static IAlert Transform(this IAlert alert)
        {
            return Transform(alert, Factory.Transformers.Create());
        }

        public static IAlert Transform<TResult>(this IAlert alert, ITransformer<TResult> transformer)
        {
            return transformer.Transform(alert);
        }

        public static void Throw(this IAlert alert, bool transformBeforeThrowing = false)
        {
            ThrowTransform(alert, a => new AlertException(a), transformBeforeThrowing);
        }

        public static void Throw(this IAlert alert, Exception inner, bool transformBeforeThrowing = false)
        {
            ThrowTransform(alert, a => new AlertException(a, inner), transformBeforeThrowing);
        }

        public static void Throw<TAlertException>(this IAlert alert, 
            Func<IAlert, TAlertException> constructor, 
            bool transformBeforeThrowing = false) 
            where TAlertException : AlertException
        {
            ThrowTransform(alert, constructor, transformBeforeThrowing);
        } 

        public static void Throw<TAlertException>(this IAlert alert, 
            Func<IAlert, Exception, TAlertException> constructor, 
            Exception inner, 
            bool transformBeforeThrowing = false)
            where TAlertException : AlertException
        {
            ThrowTransform(alert, a => constructor(a, inner), transformBeforeThrowing); 
        }

        private static void ThrowTransform<TAlertException>(IAlert alert, 
            Func<IAlert, TAlertException> constructor, 
            bool transformBeforeThrowing = false)
            where TAlertException : AlertException
        {
            if (transformBeforeThrowing)
                alert = alert.Transform();

            throw constructor(alert);
        }


        public static IEnumerable<IAlertItem> AllItems(this IAlert alert)
        {
            //HACK: may not be the most efficent
            var stack = new Stack<IAlertItem>(alert);
            while (stack.Any())
            {
                var alertItem = stack.Pop();
                yield return alertItem;

                //Add children to stack
                var stackAlert = alertItem as IAlert;
                if (stackAlert != null)
                    foreach (var item in stackAlert) 
                        stack.Push(item);

                //Add group values
                var alertGroup = alertItem as AlertItem;
                if(alertGroup != null)
                    foreach(var item in alertGroup.Values.Where(v => v is IAlertItem))
                        stack.Push(item as IAlertItem);

            }
        }

        public static IEnumerable<object> AllValues(this IAlert alert)
        {
            return from item in alert.AllItems()
                   where item is AlertItem
                   from value in (item as AlertItem).Values
                   where !(value is IAlert)
                   select value;
        }
   
        //====================================================
        //BUILDER PASS THROUGHS FOR EASE OF USE
        //====================================================
         
        public static IAlert Transform(this IAlertBuilder alertBuilder)
        {
            return Transform(alertBuilder.ToAlert());
        }

        public static IAlert Transform<TResult>(this IAlertBuilder alertBuilder, ITransformer<TResult> transformer)
        {
             return Transform(alertBuilder.ToAlert(), transformer);
        }

        public static void Throw(this IAlertBuilder alertBuilder, bool transformBeforeThrowing = false)
        {
            Throw(alertBuilder.ToAlert(), transformBeforeThrowing);
        }

        public static void Throw(this IAlertBuilder alertBuilder, Exception inner, bool transformBeforeThrowing = false)
        {
            Throw(alertBuilder.ToAlert(), inner, transformBeforeThrowing);
        }

        public static void Throw<TAlertException>(this IAlertBuilder alertBuilder, 
            Func<IAlert, TAlertException> constructor, 
            bool transformBeforeThrowing = false) 
            where TAlertException : AlertException
        {
            Throw(alertBuilder.ToAlert(), constructor, transformBeforeThrowing);
        }

        public static void Throw<TAlertException>(this IAlertBuilder alertBuilder,
            Func<IAlert, Exception, TAlertException> constructor, 
            Exception inner, 
            bool transformBeforeThrowing = false) 
            where TAlertException : AlertException
        {
            Throw(alertBuilder.ToAlert(), constructor, inner, transformBeforeThrowing);
        }

    }
}
