using System;
using System.Collections.Generic;
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
        private ContentReference _contentReference = new ContentReference(1000);
        private PageReference _pageReference = new PageReference(2000);
        private IEnumerable<string> _strings = new string[0];
        private IEnumerable<int> _ints = new int[0];
        private IEnumerable<double> _doubles = new double[0];
        private IEnumerable<DateTime> _dateTimes = new DateTime[0];

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

        public StandardPageBuilder WithContentReference(ContentReference c)
        {
            this._contentReference = c;
            return this;
        }

        public StandardPageBuilder WithPageReference(PageReference c)
        {
            this._pageReference = c;
            return this;
        }

        public StandardPageBuilder WithStrings(IEnumerable<string> s)
        {
            this._strings = s;
            return this;
        }

        public StandardPageBuilder WithInts(IEnumerable<int> i)
        {
            this._ints = i;
            return this;
        }

        public StandardPageBuilder WithDoubles(IEnumerable<double> d)
        {
            this._doubles = d;
            return this;
        }

        public StandardPageBuilder WithDateTimes(IEnumerable<DateTime> d)
        {
            this._dateTimes = d;
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
                MainVideo = _mainVideo,
                ContentReference =  _contentReference,
                PageReference = _pageReference,
                Strings = _strings,
                Ints = _ints,
                Doubles = _doubles,
                DateTimes = _dateTimes
            };
        }
    }
}
