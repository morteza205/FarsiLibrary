using System;
using System.Collections;
using System.Globalization;
using FarsiLibrary.UnitTest.Helpers;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Exceptions;
using FarsiLibrary.Utils.Internals;
using NUnit.Framework;

namespace FarsiLibrary.UnitTest
{
    [TestFixture]
    public class PersianDateTest
    {
        [Test]
        public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_Invariant_Thread_Culture()
        {
            Assert.DoesNotThrow(() => PersianDate.Parse("1388/02/30"));
        }

        [Test]
        public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_System_Persian_Culture()
        {
            using(new CultureSwitchContext(CultureHelper.FarsiCulture))
            {
                Assert.DoesNotThrow(() => PersianDate.Parse("1388/02/30"));
                Assert.DoesNotThrow(() => PersianDate.Parse("1388/02/31"));
            }
        }

        [Test]
        public void Parsing_With_Years_Less_Than_Two_Digits_Will_Use_Four_Year_Digits()
        {
            using(new CultureSwitchContext(CultureHelper.FarsiCulture))
            {
                var pd = PersianDate.Parse("88/2/3");

                Assert.That(pd.Year, Is.EqualTo(1388));
                Assert.That(pd.Month, Is.EqualTo(2));
                Assert.That(pd.Day, Is.EqualTo(3));
            }
        }

        [Test]
        public void Seven_Days_Of_Week_Validity()
        {
            var pd1 = new PersianDate(1386, 6, 1);
            Assert.AreEqual(pd1.DayOfWeek, DayOfWeek.Thursday);

            var pd2 = new PersianDate(1386, 6, 2);
            Assert.AreEqual(pd2.DayOfWeek, DayOfWeek.Friday);

            var pd3 = new PersianDate(1386, 6, 3);
            Assert.AreEqual(pd3.DayOfWeek, DayOfWeek.Saturday);

            var pd4 = new PersianDate(1386, 6, 4);
            Assert.AreEqual(pd4.DayOfWeek, DayOfWeek.Sunday);

            var pd5 = new PersianDate(1386, 6, 5);
            Assert.AreEqual(pd5.DayOfWeek, DayOfWeek.Monday);

            var pd6 = new PersianDate(1386, 6, 6);
            Assert.AreEqual(pd6.DayOfWeek, DayOfWeek.Tuesday);

            var pd7 = new PersianDate(1386, 6, 7);
            Assert.AreEqual(pd7.DayOfWeek, DayOfWeek.Wednesday);
        }

        [Test]
        public void Seven_DaysOfWeek_Index_Value_Validity()
        {
            var pd1 = new PersianDate(1387, 8, 2);
            Assert.AreEqual(pd1.DayOfWeek, DayOfWeek.Thursday);

            var pd2 = new PersianDate(1387, 8, 3);
            Assert.AreEqual(pd2.DayOfWeek, DayOfWeek.Friday);

            var pd3 = new PersianDate(1387, 8, 4);
            Assert.AreEqual(pd3.DayOfWeek, DayOfWeek.Saturday);

            var pd4 = new PersianDate(1387, 8, 5);
            Assert.AreEqual(pd4.DayOfWeek, DayOfWeek.Sunday);

            var pd5 = new PersianDate(1387, 8, 6);
            Assert.AreEqual(pd5.DayOfWeek, DayOfWeek.Monday);

            var pd6 = new PersianDate(1387, 8, 7);
            Assert.AreEqual(pd6.DayOfWeek, DayOfWeek.Tuesday);

            var pd7 = new PersianDate(1387, 8, 8);
            Assert.AreEqual(pd7.DayOfWeek, DayOfWeek.Wednesday);
        }

        [Test]
        public void Invalid_Day_In_PersianDate_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1387, 1, 32));
        }

        [Test]
        public void Invalid_Year_In_PersianDate_Throws()
        {
            var pd = new PersianDate(DateTime.MinValue.Year, 1, 1);
        }

        [Test]
        public void Can_Create_Minimum_PersianDate_Year_Value()
        {
            var pd = new PersianDate(PersianDate.MinValue.Year, 1, 1);
        }

        [Test]
        public void Can_Create_Maximum_PersianDate_Year_Value()
        {
            var pd = new PersianDate(PersianDate.MaxValue.Year, 1, 1);
        }

        [Test]
        public void Can_Cast_DateTime_To_PersianDate()
        {
            var dt = DateTime.Now;
            var pd = (PersianDate)dt;

            Assert.NotNull(pd);
        }

        [Test]
        public void Can_Cast_Nullable_DateTime_To_PersianDate()
        {
            DateTime? dt = null;
            DateTime? dtNow = DateTime.Now;

            PersianDate pdNull = dt;
            PersianDate pdNow = dtNow;

            Assert.That(pdNull, Is.Null);
            Assert.That(pdNow, Is.Not.Null);
        }

        [Test]
        public void Can_Cast_PersianDate_To_DateTime()
        {
            var pd = PersianDate.Now;
            var dt = (DateTime)pd;

            Assert.NotNull(dt);
        }

        [Test]
        public void Can_Cast_DateTime_MinValue_To_PersianDate()
        {
            var dt = DateTime.MinValue;
            var pd = (PersianDate)dt;
        }

        [Test]
        public void Can_Cast_DateTime_MaxValue_To_PersianDate()
        {
            var dt = DateTime.MaxValue;
            var pd = (PersianDate)dt;            
        }

        [Test]
        public void Compare_PersianDate_With_Equals_Method()
        {
            PersianDate pd1 = new PersianDate(1384, 10, 1);
            PersianDate pd2 = new PersianDate(1389, 12, 25);
            PersianDate pd3 = new PersianDate(1389, 12, 25);

            Assert.True(pd2.Equals((object)pd3));
            Assert.True(pd2.Equals(pd3));
            Assert.False(pd1.Equals(pd2));
        }

        [Test]
        public void Compare_PersianDate_With_Equals_Method_With_Invalid_Type()
        {
            PersianDate pd1 = PersianDate.Now;
            Assert.False(pd1.Equals(string.Empty));
        }

        [Test]
        public void Compare_PersianDate_With_Comparer_Method()
        {
            PersianDate pd1 = new PersianDate(1384, 10, 1);
            PersianDate pd2 = new PersianDate(1389, 12, 25);
            PersianDate pd3 = new PersianDate(1389, 12, 25);

            Assert.AreEqual(1, pd1.Compare(pd2, pd1));
            Assert.AreEqual(-1, pd2.Compare(pd1, pd2));
            Assert.AreEqual(0, pd1.Compare(pd1, pd1));
        }

        [Test]
        public void Compare_PersianDate_With_CompareTo_Method()
        {
            PersianDate pd1 = new PersianDate(1384, 10, 1);
            PersianDate pd2 = new PersianDate(1389, 12, 25);

            Assert.AreEqual(1, pd2.CompareTo(pd1));
            Assert.AreEqual(-1, pd1.CompareTo(pd2));
            Assert.AreEqual(-1, pd1.CompareTo(pd2));
        }

        [Test]
        public void Compare_PersianDate_With_Null_Using_Operator_Throws()
        {
            PersianDate pd1 = PersianDate.Now;
            PersianDate pd2 = null;

            Assert.Throws<InvalidOperationException>(() => { var result = pd1 > pd2; });
        }

        [Test]
        public void Compare_PersianDate_With_Null_Using_IComparer_Throws()
        {
            var pd1 = PersianDate.Now;
            object o = null;
            IComparer cmp = pd1;

            Assert.Throws<InvalidOperationException>(() => cmp.Compare(pd1, o));
        }

        [Test]
        public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparer_Throws()
        {
            var pd1 = PersianDate.Now;
            object o = "test";
            IComparer cmp = pd1;

            Assert.Throws<InvalidOperationException>(() => cmp.Compare(pd1, o));
            Assert.Throws<InvalidOperationException>(() => cmp.Compare(o, pd1));
        }

        [Test]
        public void Compare_Smaller_And_Larger_PersainDates_Using_Comparer()
        {
            var pd1 = (IComparer)new PersianDate(1380, 1, 1);
            var pd2 = (IComparer)new PersianDate(1385, 1, 1);

            Assert.AreEqual(-1, pd1.Compare(pd1, pd2));
            Assert.AreEqual(1, pd2.Compare(pd2, pd1));
        }

        [Test]
        public void Compare_PersianDate_Equality_Using_Comparer()
        {
            var pd1 = (IComparer)new PersianDate(1380, 1, 1);
            var pd2 = ((ICloneable) pd1).Clone();

            Assert.AreEqual(0, pd1.Compare(pd1, pd2));
        }

        [Test]
        public void Compare_PersianDate_Using_IComparer()
        {
            var pd1 = PersianDate.Now;
            var pd2 = new PersianDate(1380, 1, 1);
            IComparer cmp = pd1;

            var result = cmp.Compare(pd1, pd2);
        }

        [Test]
        public void Compare_PersianDate_Using_IComparable()
        {
            var pd1 = (IComparable)new PersianDate(1390, 1, 1);
            var pd2 = (IComparable)new PersianDate(1380, 1, 1);

            Assert.AreEqual(1, pd1.CompareTo(pd2));
            Assert.AreEqual(-1, pd2.CompareTo(pd1));
            Assert.AreEqual(0, pd2.CompareTo(pd2));
        }

        [Test]
        public void Compare_PersianDate_With_Null_Using_IComparable_Throws()
        {
            var pd1 = PersianDate.Now;
            object o = null;
            IComparable cmp = pd1;

            Assert.Throws<InvalidOperationException>(() => cmp.CompareTo(o));
        }

        [Test]
        public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparable_Throws()
        {
            var pd1 = PersianDate.Now;
            object o = "test";
            IComparable cmp = pd1;

            Assert.Throws<InvalidOperationException>(() => cmp.CompareTo(o));
        }

        [Test]
        public void Can_Get_Localized_Month_Name()
        {
            var pd = new PersianDate(1380, 1, 1);
            var monthName = pd.LocalizedMonthName;

            Assert.AreEqual(monthName, PersianDateTimeFormatInfo.MonthNames[0]);
        }

        [Test]
        public void Creating_With_Invalid_Date_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 0, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 13, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1388, 12, 30));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(10000, 1, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 7, 31));
        }

        [Test]
        public void Creating_With_Invalid_Time_Throws()
        {
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 30, 1, 1, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 70, 1, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 1, 100, 1));
            Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 1, 1, 2000));
        }

        [Test]
        public void Can_Create_With_HourAndMinute()
        {
            PersianDate pd = new PersianDate(1384, 1, 1, 20, 30);

            Assert.AreEqual(0, pd.Second);
            Assert.AreEqual(0, pd.Millisecond);
        }

        [Test]
        public void Creating_With_Default_Constructor_Creates_Now()
        {
            PersianDate now1 = PersianDate.Now;
            PersianDate now2 = new PersianDate();

            Assert.AreEqual(now1.Year, now2.Year);
            Assert.AreEqual(now1.Month, now2.Month);
            Assert.AreEqual(now1.Day, now2.Day);
        }

        [Test]
        public void Can_Create_With_String_And_Time()
        {
            PersianDate pd = new PersianDate("1384/01/01", new TimeSpan(12, 23, 48));

            Assert.AreEqual(1384, pd.Year);
            Assert.AreEqual(1, pd.Month);
            Assert.AreEqual(1, pd.Day);
            Assert.AreEqual(12, pd.Hour);
            Assert.AreEqual(23, pd.Minute);
            Assert.AreEqual(48, pd.Second);
            Assert.AreEqual(0, pd.Millisecond);
        }

        [Test]
        public void Can_Create_With_String_And_Long_Date_Format()
        {
            string datestring = "1384/01/03 12:22:43 AM";
            PersianDate pd = new PersianDate(datestring);

            Assert.AreEqual(1384, pd.Year);
            Assert.AreEqual(1, pd.Month);
            Assert.AreEqual(3, pd.Day);
            Assert.AreEqual(0, pd.Hour);
            Assert.AreEqual(22, pd.Minute);
            Assert.AreEqual(43, pd.Second);
            Assert.AreEqual(0, pd.Millisecond);
        }

        [Test]
        public void Can_Create_With_String_And_Short_Date_Format()
        {
            string datestring = "1384/01/03 12:22 PM";
            PersianDate pd = new PersianDate(datestring);

            Assert.AreEqual(1384, pd.Year);
            Assert.AreEqual(1, pd.Month);
            Assert.AreEqual(3, pd.Day);
            Assert.AreEqual(12, pd.Hour);
            Assert.AreEqual(22, pd.Minute);
            Assert.AreEqual(0, pd.Second);
            Assert.AreEqual(0, pd.Millisecond);
        }

        [Test]
        public void Can_Create_With_String_And_Date_Formatting()
        {
            string datestring = "1384/01/03";
            PersianDate pd = new PersianDate(datestring);

            Assert.AreEqual(1384, pd.Year);
            Assert.AreEqual(1, pd.Month);
            Assert.AreEqual(3, pd.Day);
            Assert.AreEqual(0, pd.Hour);
            Assert.AreEqual(0, pd.Minute);
            Assert.AreEqual(0, pd.Second);
            Assert.AreEqual(0, pd.Millisecond);
        }

        [Test]
        public void ToString_With_Empty_Parameter_Returns_Generic_Format()
        {
            var pd = new PersianDate(1380, 1, 1);
            var am = PersianDateTimeFormatInfo.AMDesignator;
            var result1 = pd.ToString();
            var result2 = pd.ToString(null, null);

            Assert.AreEqual("1380/01/01 00:00:00 " + am, pd.ToString());
            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void ToString_With_General_Parameter_Returns_Date_And_Time()
        {
            var pd = new PersianDate(1380, 1, 1);
            var am = PersianDateTimeFormatInfo.AMDesignator;

            Assert.AreEqual("1380/01/01 00:00:00 " + am, pd.ToString("G"));
        }

        [Test]
        public void Compare_PersianDate_Using_Equal_Operator()
        {
            PersianDate pd1 = null;
            PersianDate pd2 = null;
            PersianDate pd3 = new PersianDate(1380, 1, 1);

            Assert.True(pd1 == pd2);
            Assert.True(pd1 != pd3);
            Assert.True(pd3 != pd1);

            Assert.False(pd1 != pd2);
            Assert.False(pd1 == pd3);
            Assert.False(pd3 == pd1);
        }

        [Test]
        public void Compare_PersianDate_Using_Greater_Equal_Operator_And_Null_Throws()
        {
            PersianDate pd1 = null;
            PersianDate pd2 = null;
            PersianDate pd3 = new PersianDate(1380, 1, 1);
            bool result;

            Assert.Throws<InvalidOperationException>(() => result = pd1 <= pd3);
            Assert.Throws<InvalidOperationException>(() => result = pd2 >= pd3);
        }

        [Test]
        public void Compare_PersianDate_Using_Not_Equal_Operator()
        {
            PersianDate pd1 = new PersianDate(1380, 11, 23);
            PersianDate pd2 = new PersianDate(1384, 1, 30);

            Assert.True(pd1 != pd2);
        }

        [Test]
        public void Compare_PersianDate_Using_Less_Than_Operator()
        {
            PersianDate pd2 = new PersianDate(1380, 11, 23);
            PersianDate pd1 = new PersianDate(1384, 1, 30);

            Assert.True(pd2 < pd1);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_With_Null_Throws()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 30);
            PersianDate pd2 = null;
            bool result;

            Assert.Throws<InvalidOperationException>(() => result = pd1 < pd2);
            Assert.Throws<InvalidOperationException>(() => result = pd2 < pd1);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Year()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 30);
            PersianDate pd2 = new PersianDate(1385, 1, 30);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Month()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 30);
            PersianDate pd2 = new PersianDate(1384, 2, 30);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Day()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29);
            PersianDate pd2 = new PersianDate(1384, 1, 30);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Hour()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29, 20, 0);
            PersianDate pd2 = new PersianDate(1384, 1, 29, 22, 0);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_Persian_Date_Using_Less_Than_Operator_Differing_Minute()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29, 20, 20);
            PersianDate pd2 = new PersianDate(1384, 1, 29, 20, 35);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Second()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29, 20, 20, 10);
            PersianDate pd2 = new PersianDate(1384, 1, 29, 20, 20, 15);

            CheckDates(pd1, pd2);
        }

        [Test]
        public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Millisecond()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
            PersianDate pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 22);

            CheckDates(pd1, pd2);
        }

        private void CheckDates(PersianDate smaller, PersianDate greater)
        {
            Assert.True(smaller < greater);
            Assert.True(greater > smaller);

            Assert.False(smaller > greater);
            Assert.False(greater < smaller);            
        }

        [Test]
        public void Comparing_Equal_PersianDates_Using_CompareTo()
        {
            PersianDate pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
            PersianDate pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);

            var diff = pd1.CompareTo(pd2);

            Assert.AreEqual(0, diff);
        }

        [Test]
        public void PersianDate_Instance_Creates_StringBased_HashCode()
        {
            PersianDate pd = PersianDate.Now;
            int hashcode = pd.ToString("s").GetHashCode();

            Assert.AreEqual(hashcode, pd.GetHashCode());
        }

        [Test]
        public void Can_Parse_Short_Date_String()
        {
            var value = "1384/03/01";
            var pd = PersianDate.Parse(value);

            Assert.AreEqual(1384, pd.Year);
            Assert.AreEqual(3, pd.Month);
            Assert.AreEqual(1, pd.Day);
        }

        [Test]
        public void Converting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
        {
            var minValue = PersianDate.MinValue;
            var maxValue = PersianDate.MaxValue;
            var dateMinValue = DateTime.MinValue;
            var dateMaxValue = DateTime.MaxValue;

            Assert.That(minValue.ToPersianDate(), Is.Not.Null);
            Assert.That(maxValue.ToPersianDate(), Is.Not.Null);
            Assert.That(dateMaxValue.ToPersianDate(), Is.Not.Null);
            Assert.That(dateMinValue.ToPersianDate(), Is.Null);
        }

        [Test]
        public void Casting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
        {
            var minValue = PersianDate.MinValue;
            var maxValue = PersianDate.MaxValue;
            var dateMinValue = DateTime.MinValue;
            var dateMaxValue = DateTime.MaxValue;

            Assert.That((PersianDate)minValue, Is.Not.Null);
            Assert.That((PersianDate)maxValue, Is.Not.Null);
            Assert.That((PersianDate)dateMaxValue, Is.Not.Null);
            Assert.That((PersianDate)dateMinValue, Is.Null);            
        }

        [Test]
        public void Can_Parse_Date_Short_Time_String()
        {
            var value = "1394/05/01 12:33:00 AM";
            var pd = PersianDate.Parse(value);

            Assert.AreEqual(1394, pd.Year);
            Assert.AreEqual(5, pd.Month);
            Assert.AreEqual(1, pd.Day);
            Assert.AreEqual(0, pd.Hour);
            Assert.AreEqual(33, pd.Minute);
        }

        [Test]
        public void Parsing_Empty_String_Will_Throw()
        {
            Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(string.Empty));
        }

        [Test]
        public void Can_Get_Weekday_Name()
        {
            PersianDate pd = new PersianDate(1387, 7, 7); //Yekshanbeh
            Assert.AreEqual(PersianDateTimeFormatInfo.GetWeekDay(DayOfWeek.Sunday), pd.LocalizedWeekDayName);
        }

        [Test]
        public void Comparing_Null_PersianDate_Should_Be_Equal()
        {
            PersianDate pd1 = null;
            PersianDate pd2 = null;

            Assert.False(pd1 > pd2);
            Assert.False(pd1 < pd2);
            Assert.False(pd2 > pd1);
            Assert.False(pd2 < pd1);
        }

        [Test]
        public void ToString_With_Custom_FormatProvider()
        {
            string format = "CustomYearMonth";
            PersianDate pd = new PersianDate(1383, 4, 5);
            var value = pd.ToString(format, new TestFormatProvider());

            Assert.AreEqual(pd.Year + " -- " + pd.Month, value);
        }

        [Test]
        public void ToString_With_Custom_FormatProvider_And_No_Specific_Format_Uses_Generic()
        {
            PersianDate pd = new PersianDate(1383, 4, 5);
            var value1 = pd.ToString(new TestFormatProvider());
            var value2 = pd.ToString("G");

            Assert.AreEqual(value2, value1);
        }

        [Test]
        public void Parse_With_Invalid_Or_Null_Values_Throws()
        {
            Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(null));
            Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(string.Empty));
            Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse("1234"));
        }

        [Test]
        public void Try_Parse_With_Invalid_Or_Null_Values_Returns_False()
        {
            PersianDate pd = null;
            bool canParse = false;
            
            canParse = PersianDate.TryParse(null, out pd);
            Assert.False(canParse);
            Assert.Null(pd);

            canParse = PersianDate.TryParse(string.Empty, out pd);
            Assert.False(canParse);
            Assert.Null(pd);

            canParse = PersianDate.TryParse("1234", out pd);
            Assert.False(canParse);
            Assert.Null(pd);
        }

        [Test]
        public void Try_Parse_With_Correct_Value_Returns_True()
        {
            PersianDate pd = null;
            bool canParse = false;

            canParse = PersianDate.TryParse("1384/1/1", out pd);
            Assert.True(canParse);
            Assert.NotNull(pd);
        }

        [Test]
        public void Can_Convert_To_Written()
        {
            PersianDate pd = new PersianDate("1384/1/1");
            string written = pd.ToWritten();

            Assert.AreEqual(string.Format("{0} {1} {2} {3}", pd.LocalizedWeekDayName, pd.Day, pd.LocalizedMonthName, pd.Year), written);
        }

        [Test]
        public void Can_Parse_Date_From_String()
        {
            using(new CultureSwitchContext(CultureHelper.PersianCulture))
            {
                var dateString = "1388/06/17 12:00:00 ق.ظ";
                var pd = PersianDate.Parse(dateString);

                Assert.That(pd.Year, Is.EqualTo(1388));
                Assert.That(pd.Month, Is.EqualTo(6));
                Assert.That(pd.Day, Is.EqualTo(17));
            }
        }

        [Test]
        public void Can_Parse_Date_From_String_Using_BuiltIn_CultureInfo()
        {
            using (new CultureSwitchContext(new CultureInfo("fa-ir")))
            {
                var dateString = "1388/06/17 12:00:00 ق.ظ";
                var pd = PersianDate.Parse(dateString);

                Assert.That(pd.Year, Is.EqualTo(1388));
                Assert.That(pd.Month, Is.EqualTo(6));
                Assert.That(pd.Day, Is.EqualTo(17));
            }
        }
    }
}