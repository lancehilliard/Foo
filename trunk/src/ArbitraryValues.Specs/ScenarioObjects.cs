// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective

using System;
using System.Collections.Generic;
using System.Reflection;
using ArbitraryValues.FakeMakers;
using Machine.Specifications;
using Rhino.Mocks;

// ReSharper restore RedundantUsingDirective

namespace ArbitraryValues.Specs
{
    /**
     * The purpose of Scenario Objects is to provide a place to create Fakes,DeliberateValues,ArbitraryValues, and SystemsUnderTest 
     * to remove redundant and repetitive object from Specifications.
     * The Establish will be ran before each specification     
     */
    public class ScenarioObjects
    {
        //buys us some time by only setting these up once
        static ScenarioObjects()
        {
            //assign deliberates here without method call.  This will allow readonly status to prevent bad tests that assign values to deliberates (can cause unexpected side-effects based on order tests run in).
            AssignArbitraryValues();
        }

        //prefer this to Context because it will never change between mspec versions and is possibly extensible to other frameworks
        protected ScenarioObjects()
        {

            AssignFakes();
            AssignSystemsUnderTest();
        }

        static void AssignFakes()
        {
            Foo.AssignFakes<ScenarioObjects>(FakeMaker.MakeRhinoMocksFake);
        }

        static void AssignArbitraryValues()
        {
            Foo.AssignArbitraryValues<ScenarioObjects>();
        }

        static void AssignSystemsUnderTest() { }
    }

    /***
     * The generic Result type is used when the System Under Test returns a value that you would like to use in an assertion.
     */
    public class ScenarioObjects<ResultType> : ScenarioObjects
    {
        protected static ResultType Result;
    }
}