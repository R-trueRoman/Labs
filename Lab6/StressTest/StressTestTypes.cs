using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StressTest
{
    public enum Material { StainlessSteel, Aluminium, ReinforcedConcrete, Composite, Titanium }

    public enum CrossSection { IBeam, Box, ZShaped, CShaped }

    public enum TestResult { Pass, Fail }

    public struct TestCaseResult
    {

        public TestResult Result;

        public string ReasonForFailure;
    }

    public class StressTestCase
    {

        public Material GirderMaterial;

        public CrossSection CrossSection;

        public int LengthInMm;

        public int HeightInMm;

        public int WidthInMm;

        public TestCaseResult? TestCaseResult;

        public StressTestCase() : this(Material.StainlessSteel, CrossSection.IBeam, 4000, 20, 15) { }

        public StressTestCase(Material girderMaterial, CrossSection crossSection, int lengthInMm, int heightInMm, int widthInMm)
        {
            this.GirderMaterial = girderMaterial;
            this.CrossSection = crossSection;
            this.LengthInMm = lengthInMm;
            this.HeightInMm = heightInMm;
            this.WidthInMm = widthInMm;
            this.TestCaseResult = null;
        }

        public void PerformStressTest()
        {
            TestCaseResult currentTestCase = new TestCaseResult();

            string[] failureReasons = { "Fracture detected", "Beam snapped", "Beam dimensions wrong", "Beam warped", "Other" };

            if (Utility.rand.Next(10) == 9)
            {
                currentTestCase.Result = TestResult.Fail;
                currentTestCase.ReasonForFailure = failureReasons[Utility.rand.Next(5)];
                TestCaseResult = currentTestCase;
            }
            else
            {
                currentTestCase.Result = TestResult.Pass;
                TestCaseResult = currentTestCase;
            }
        }

        public TestCaseResult? GetStressTestResult()
        {
            return TestCaseResult;
        }

        public override string ToString()
        {
            return string.Format("Material: {0}, CrossSection: {1}, Length: {2}mm, Height: {3}mm, Width: {4}mm",
                GirderMaterial.ToString(), CrossSection.ToString(), LengthInMm, HeightInMm, WidthInMm);
        }

    }
}
