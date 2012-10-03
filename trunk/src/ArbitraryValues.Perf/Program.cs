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
            var mocksCount = 100;
            var fakesDurations = new List<DurationData>();
            fakesDurations.Add(DurationGetter.Get("Rhino Mocks", mocksCount, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeRhinoMocksFake)));
            fakesDurations.Add(DurationGetter.Get("Moq        ", mocksCount, () => Foo.AssignFakes<MoqFakes>(FakeMaker.MakeMoqFake)));
            fakesDurations.Add(DurationGetter.Get("NSubstitute", mocksCount, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeNSubstituteFake)));
            fakesDurations.Add(DurationGetter.Get("FakeItEasy ", mocksCount, () => Foo.AssignFakes<SimpleFakes>(FakeMaker.MakeFakeItEasyFake)));

            var typesCount = 100;
            var typeDurations = new List<DurationData>();
            typeDurations.Add(DurationGetter.Get("XmlDocument", typesCount, () => Foo.Get<XmlDocument>()));
            typeDurations.Add(DurationGetter.Get("double     ", typesCount, () => Foo.Get<double>()));
            typeDurations.Add(DurationGetter.Get("int        ", typesCount, () => Foo.Get<int>()));
            typeDurations.Add(DurationGetter.Get("float      ", typesCount, () => Foo.Get<float>()));
            typeDurations.Add(DurationGetter.Get("Exception  ", typesCount, () => Foo.Get<Exception>()));

            var lazysCount = 100;
            var lazyDurations = new List<DurationData>();
            lazyDurations.Add(DurationGetter.Get("Lazy<XmlDocument>", lazysCount, () => Foo.Get<Lazy<XmlDocument>>()));
            lazyDurations.Add(DurationGetter.Get("Lazy<double>     ", lazysCount, () => Foo.Get<Lazy<double>>()));
            lazyDurations.Add(DurationGetter.Get("Lazy<int>        ", lazysCount, () => Foo.Get<Lazy<int>>()));
            lazyDurations.Add(DurationGetter.Get("Lazy<float>      ", lazysCount, () => Foo.Get<Lazy<float>>()));
            lazyDurations.Add(DurationGetter.Get("Lazy<Exception>  ", lazysCount, () => Foo.Get<Lazy<Exception>>()));

            Console.WriteLine("ArbitraryValues Performance");
            Console.WriteLine("");
            ReportOnDurations(fakesDurations);
            ReportOnDurations(typeDurations);
            ReportOnDurations(lazyDurations);
            stopwatch.Stop();
            Console.WriteLine("Press enter to exit... (Total running time: roughly {0} seconds", Math.Floor(stopwatch.Elapsed.TotalSeconds));
            Console.ReadLine();
        }

        static void ReportOnDurations(List<DurationData> durations) {
            var orderedDurations = durations.OrderBy(x => x.Duration);
            foreach (var durationData in orderedDurations) {
                ReportOnDuration(durationData);
            }

            var whitespace = new string(' ', durations.Max(x => x.Title.Length) + durations.Max(x => x.Duration.TotalMilliseconds.ToString().Length));
            var totalTimeSpan = new TimeSpan(durations.Sum(x => x.Duration.Ticks));
            var averageTimeSpan = new TimeSpan(durations.Sum(x => x.Duration.Ticks)/2);
            //Console.WriteLine("{0}{1} ({2} Avg)", whitespace, FormatTimeSpan(totalTimeSpan), FormatTimeSpan(averageTimeSpan));
            Console.WriteLine("{0}({1} Total)", whitespace, FormatTimeSpan(totalTimeSpan));
            Console.WriteLine("");
        }

        static void ReportOnDuration(DurationData durationData) {
            Console.WriteLine("{0}: {1}", durationData.Title, FormatTimeSpan(durationData.Duration));
        }

        static string FormatTimeSpan(TimeSpan timeSpan) {
            var result = string.Format("{0}ms", Math.Floor(timeSpan.TotalMilliseconds));
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
