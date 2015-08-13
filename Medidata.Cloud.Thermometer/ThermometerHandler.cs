using System;
using Microsoft.Owin;

namespace Medidata.Cloud.Thermometer
{
    internal class ThermometerHandler : IThermometerHandler
    {
        public IThermometerQuestion Question { get; private set; }

        public Func<IThermometerQuestion, object> Handler { get; private set; }

        public ThermometerHandler(IThermometerQuestion question, Func<IThermometerQuestion, object> handler)
        {
            if (question == null) throw new ArgumentNullException("question");
            if (handler == null) throw new ArgumentNullException("handler");
            Question = question;
            Handler = handler;
        }
    }
}
