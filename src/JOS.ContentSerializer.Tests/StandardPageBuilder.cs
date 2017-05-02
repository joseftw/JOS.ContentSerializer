using System;
using EPiServer.Core;
using JOS.ContentSerializer.Tests.Pages;
using Ploeh.AutoFixture;

namespace JOS.ContentSerializer.Tests
{
    public class StandardPageBuilder
    {
        private string _heading = new Fixture().Create<string>();
        private string _description = new Fixture().Create<string>();
        private int _age = new Fixture().Create<int>();
        private double _degrees = new Fixture().Create<double>();
        private DateTime _starting = new Fixture().Create<DateTime>();
        private bool _private = new Fixture().Create<bool>();
        private XhtmlString _mainBody = new XhtmlString("<h1>This is <b>HTML</b> </h1>");
        private ContentArea _mainContentArea = new ContentArea(); // TODO USE BUILDER HERE
        private VideoBlock _mainVideo = new VideoBlock(); // TODO USE BUILDER HERE.

        public StandardPageBuilder WithHeading(string h)
        {
            this._heading = h;
            return this;
        }

        public StandardPageBuilder WithDescription(string d)
        {
            this._description = d;
            return this;
        }

        public StandardPageBuilder WithAge(int a)
        {
            this._age = a;
            return this;
        }

        public StandardPageBuilder WithDegrees(double d)
        {
            this._degrees = d;
            return this;
        }

        public StandardPageBuilder WithStarting(DateTime s)
        {
            this._starting = s;
            return this;
        }

        public StandardPageBuilder WithPrivate(bool p)
        {
            this._private = p;
            return this;
        }

        public StandardPageBuilder WithMainBody(XhtmlString m)
        {
            this._mainBody = m;
            return this;
        }

        public StandardPageBuilder WithMainBody(string m)
        {
            this._mainBody = new XhtmlString(m);
            return this;
        }

        public StandardPageBuilder WithMainContentArea(ContentArea m)
        {
            this._mainContentArea = m;
            return this;
        }

        public StandardPageBuilder WithMainContentArea(string m)
        {
            this._mainContentArea = new ContentArea(m);
            return this;
        }

        public StandardPageBuilder WithMainVideo(VideoBlock v)
        {
            this._mainVideo = v;
            return this;
        }

        public StandardPage Build()
        {
            return new StandardPage
            {
                Heading = _heading,
                Description = _description,
                Age = _age,
                Degrees = _degrees,
                Private = _private,
                Starting = _starting,
                MainBody = _mainBody,
                MainContentArea = _mainContentArea,
                MainVideo = _mainVideo
            };
        }
    }
}
