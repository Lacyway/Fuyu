using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuyu.Common.Networking;

public class Router<TController, TContext> where TController : IRouterController<TContext>
    where TContext : IRouterContext
{
    public List<TController> Controllers { get; private set; }

    public Router()
    {
        Controllers = new List<TController>();
    }

    public T AddController<T>() where T : TController, new()
    {
        T controller = new T();
        Controllers.Add(controller);
        return controller;
    }

    public T GetController<T>() where T : TController
    {
        return (T)Controllers.Find(c => c is T);
    }

    public T RemoveController<T>() where T : TController
    {
        var controller = Controllers.Find(c => c is T);
        if (controller != null)
        {
            Controllers.Remove(controller);
        }

        return (T)controller;
    }

    public TTo ReplaceController<TFrom, TTo>(out TFrom old)
        where TFrom : TController
        where TTo : TController, new()
    {
        old = RemoveController<TFrom>();

        return AddController<TTo>();
    }

    public List<TController> GetAllMatching(TContext context)
    {
        var matches = new List<TController>();

        foreach (var controller in Controllers)
        {
            if (controller.IsMatch(context))
            {
                matches.Add(controller);
            }
        }

        if (matches.Count == 0)
        {
            throw new RouteNotFoundException($"No match on context {context}");
        }

        // NOTE: do we want to support multi-matching?
        // -- seionmoya, 2024/09/02
        if (matches.Count > 1)
        {
            throw new Exception($"Too many matches on context {context}");
        }

        return matches;
    }

    public virtual async Task RouteAsync(TContext context)
    {
        var matches = GetAllMatching(context);
        foreach (var match in matches)
        {
            await match.RunAsync(context);
        }
    }
}