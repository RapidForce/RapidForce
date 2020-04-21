using System;
using System.Dynamic;

namespace RapidForce
{
    public interface IModel
    {
        int Handle { get; }

        void Unpack(dynamic model);

        ExpandoObject Pack();
    }
}
