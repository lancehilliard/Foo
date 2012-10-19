using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml;
using ArbitraryValues.FakeMakers;
using Moq;

namespace ArbitraryValues.Perf {
    class Program {
        static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = 20;

            //var fakesDurations = new List<DurationData>();
            //fakesDurations.Add(DurationGetter.Get("Rhino Mocks", count, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeRhinoMocksFake)));
            //fakesDurations.Add(DurationGetter.Get("Moq        ", count, () => Foo.AssignFakes<MoqFakes>(FakeMaker.MakeMoqFake)));
            //fakesDurations.Add(DurationGetter.Get("NSubstitute", count, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeNSubstituteFake)));
            //fakesDurations.Add(DurationGetter.Get("FakeItEasy ", count, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeFakeItEasyFake)));

            var typeDurations = new List<DurationData>();
            typeDurations.Add(DurationGetter.Get("Foo.Get<XmlDocument>()", count, () => Foo.Get<XmlDocument>()));
            typeDurations.Add(DurationGetter.Get("Foo.Get<double>()     ", count, () => Foo.Get<double>()));
            typeDurations.Add(DurationGetter.Get("Foo.Get<int>()        ", count, () => Foo.Get<int>()));
            typeDurations.Add(DurationGetter.Get("Foo.Get<float>()      ", count, () => Foo.Get<float>()));
            typeDurations.Add(DurationGetter.Get("Foo.Get<Exception>()  ", count, () => Foo.Get<Exception>()));

            var lazyDurations = new List<DurationData>();
            lazyDurations.Add(DurationGetter.Get("Foo.Get<Lazy<XmlDocument>>()", count, () => Foo.Get<Lazy<XmlDocument>>()));
            lazyDurations.Add(DurationGetter.Get("Foo.Get<Lazy<double>>()     ", count, () => Foo.Get<Lazy<double>>()));
            lazyDurations.Add(DurationGetter.Get("Foo.Get<Lazy<int>>()        ", count, () => Foo.Get<Lazy<int>>()));
            lazyDurations.Add(DurationGetter.Get("Foo.Get<Lazy<float>>()      ", count, () => Foo.Get<Lazy<float>>()));
            lazyDurations.Add(DurationGetter.Get("Foo.Get<Lazy<Exception>>()  ", count, () => Foo.Get<Lazy<Exception>>()));

            Console.WriteLine("ArbitraryValues Performance");
            Console.WriteLine("");
            //ReportOnDurations(fakesDurations);
            ReportOnDurations(typeDurations);
            ReportOnDurations(lazyDurations);
            stopwatch.Stop();
            Console.WriteLine("Press any key to exit... (Total running time: roughly {0}s)", Math.Ceiling(stopwatch.Elapsed.TotalSeconds));
            Console.ReadKey();
        }

        static void ReportOnDurations(List<DurationData> durations) {
            var orderedDurations = durations.OrderBy(x => x.Duration);
            foreach (var durationData in orderedDurations) {
                ReportOnDuration(durationData);
            }

            var whitespace = new string(' ', durations.Max(x => x.Title.Length) + durations.Max(x => x.Duration.TotalMilliseconds.ToString().Length));
            var totalTimeSpan = new TimeSpan(durations.Sum(x => x.Duration.Ticks));
            //var averageTimeSpan = new TimeSpan(durations.Sum(x => x.Duration.Ticks)/2);
            //Console.WriteLine("{0}{1} ({2} Avg)", whitespace, FormatTimeSpan(totalTimeSpan), FormatTimeSpan(averageTimeSpan));
            Console.WriteLine("{0}({1} Total)", whitespace, FormatTimeSpan(totalTimeSpan));
            Console.WriteLine("");
        }

        static void ReportOnDuration(DurationData durationData) {
            Console.WriteLine("{0}: {1}", durationData.Title, FormatTimeSpan(durationData.Duration));
        }

        static string FormatTimeSpan(TimeSpan timeSpan) {
            var result = string.Format("{0}ms", timeSpan.TotalMilliseconds);
            return result;
        }
    }

    public class SimpleFakes {
        protected static IDisposable DisposableFake;
    }

    public class MoqFakes {
        protected static Mock<IDisposable> DisposableMoqFake;
    }

    public class DurationGetter {
        public static DurationData Get(string actionDescription, int count, Action action) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++) {
                action();
            }
            stopwatch.Stop();
            var title = string.Format("{0} x {1}", count, actionDescription);
            var result = new DurationData(title, count, stopwatch.Elapsed);
            return result;
        }
    }

    public class DurationData {
        public string Title { get; private set; }
        public int Count { get; private set; }
        public TimeSpan Duration { get; private set; }

        public DurationData(string title, int count, TimeSpan duration) {
            Title = title;
            Count = count;
            Duration = duration;
        }
    }
}
