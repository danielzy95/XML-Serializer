﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XML_Serializer;

namespace XML_Serializer_Tests
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void Serializer_Init ()
        {
            var serializer = new XMLSerializer();

            Assert.IsNotNull(serializer, "The Serializer should not be null when initialized");
        }

        [TestMethod]
        public void Serialize_Integer()
        {
            Serialize(new[] { 100, 50, 10, -10, 2, 0, 1000, 100000, -1000, -2 }, "num");
        }

        [TestMethod]
        public void Serialize_Long()
        {
            Serialize(new[] { 100000000000000L, 50000000000000L, 1000000000000000000L,
                                     -10000000000000L, -20000000000000000L }, "num");
        }

        [TestMethod]
        public void Serialize_Float()
        {
            Serialize(new[] { 100.50f, 50.22f, 10.452f, -10.2031f, 2.393393f, 
                                     0.5f, 1000.39438f, 100000.1f, -1000.453f }, "num");
        }

        [TestMethod]
        public void Serialize_Double()
        {
            Serialize(new[] { 10001.10010110011, 1.39848498392, 
                                     -45.384482938383838, 20.299293111 }, "num");
        }

        [TestMethod]
        public void Serialize_UInt()
        {
            Serialize(new[] { 123U, 12U, 10U, 300U, 4294967290U }, "num");
        }

        [TestMethod]
        public void Serialize_UShort()
        {
            Serialize(new ushort[] { 65535, 6000, 300, 30 }, "num");
        }

        [TestMethod]
        public void Serialize_Byte()
        {
            Serialize(new byte[] { 0, 200, 255, 144, 16, 64 }, "num");
        }

        [TestMethod]
        public void Serialize_Short()
        {
            Serialize(new short[] { -100, -32768, -100, 23000, 32767 }, "num");
        }

        [TestMethod]
        public void Serialize_ULong()
        {
            Serialize(new[] { 8UL, 100000000000000000UL, 9223372036854775808UL, 8446744073709551615UL }, "num");
        }

        [TestMethod]
        public void Serialize_Decimal()
        {
            Serialize(new[] { 300.5m, 200.453m, 99.9m, 9.2m }, "num");
        }

        [TestMethod]
        public void Serialize_SByte()
        {
            Serialize(new[] { -128, -100, 127, 100, 30, 40, -50 }, "num");
        }

        [TestMethod]
        public void Serialize_String()
        {
            Serialize(new[] { "Hello!!!!", "h", "Hey", "ddijeufn3893848", "Lorem ipsum lolis" }, "string");
        }

        [TestMethod]
        public void Serialize_Char()
        {
            Serialize(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'k' }, "char");
        }

        [TestMethod]
        public void Serialize_Bool()
        {
            Serialize(new[] { true, false }, "bool");
        }

        [TestMethod]
        public void Serialize_Date_Only()
        {
            var serializer = new XMLSerializer();

            var date = new DateTime(2015, 10, 6);

            var result = serializer.Serialize(date);

            Assert.AreEqual("<date><year>2015</year><month>10</month><day>6</day>" +
                            "<hour>0</hour><minute>0</minute><second>0</second></date>", result, 
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Date_And_Time()
        {
            var serializer = new XMLSerializer();

            var date = new DateTime(2015, 10, 6, 3, 30, 15);

            var result = serializer.Serialize(date);

            Assert.AreEqual("<date><year>2015</year><month>10</month><day>6</day>" +
                            "<hour>3</hour><minute>30</minute><second>15</second></date>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class()
        {
            var serializer = new XMLSerializer();

            var person = new Person
            {
                 Id = 100,
                 FirstName = "Daniel",
                 LastName = "Zelaya",
                 Age = 20,
                 Drives = true,
                 BloodType = 'O',
                 Birthday = new DateTime(1995, 10, 1, 23, 45, 0)
            };

            var result = serializer.Serialize(person);

            Assert.AreEqual("<Person>" +
                            "<Id>100</Id><FirstName>Daniel</FirstName><LastName>Zelaya</LastName><Age>20</Age>" +
                            "<Drives>True</Drives><BloodType>O</BloodType><Birthday>10/1/1995 11:45:00 PM</Birthday>" +
                            "</Person>", 
                            result, "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class_With_Class_Properties()
        {
            var serializer = new XMLSerializer();

            var player = new Player
            {
                Username = "wupa9",
                Level = 48,
                Thievery = new Skill
                {
                    Level = 30,
                    Exp = 34590
                }
            };

            var result = serializer.Serialize(player);

            Assert.AreEqual("<Player>" +
                                "<Username>wupa9</Username>" +
                                "<Level>48</Level>" +
                                "<Thievery>" +
                                    "<Level>30</Level>" +
                                    "<Exp>34590</Exp>" +
                                "</Thievery>" +
                            "</Player>",
                            result, "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class_With_Array_Property()
        {
            var serializer = new XMLSerializer();

            var merchant = new Merchant
            {
                Name = "Al' Mazhiik",
                Race = "Khajiit",
                Inventory = new[]
                {
                    "Katana",
                    "Steel Cuirass",
                    "Leather Dagger",
                    "Health Potion"
                },
                Items = new[]
                {
                    new Item("Lol1"),
                    new Item("Lol2"),
                    new Item("Lol3"),
                    new Item("Lol4")
                }
            };

            var result = serializer.Serialize(merchant);

            var expected = "<Merchant>" +
                               "<Name>Al' Mazhiik</Name>" +
                               "<Race>Khajiit</Race>" +
                               "<Inventory>" +
                                   "<string>Katana</string>" +
                                   "<string>Steel Cuirass</string>" +
                                   "<string>Leather Dagger</string>" +
                                   "<string>Health Potion</string>" +
                               "</Inventory>" +
                               "<Items>" +
                                    "<Item><Name>Lol1</Name></Item>" +
                                    "<Item><Name>Lol2</Name></Item>" +
                                    "<Item><Name>Lol3</Name></Item>" +
                                    "<Item><Name>Lol4</Name></Item>" +
                               "</Items>" +
                           "</Merchant>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class_With_Property_Attributes()
        {
            var serializer = new XMLSerializer();

            var user = new User
            {
                Username = "wupa9",
                Password = "12345",
                Points = 100,
                MegaUltraProp = "LOL"
            };

            var result = serializer.Serialize(user);

            var expected = "<User>" +
                               "<Username>wupa9</Username>" +
                               "<Pass>12345</Pass>" +
                               "<Points>100</Points>" +
                               "<MUP>LOL</MUP>" +
                           "</User>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class_With_Fields()
        {
            var serializer = new XMLSerializer();

            var student = new Student
            {
                Name = "Johnny",
                Age = 16
            };

            var result = serializer.Serialize(student);

            var expected = "<Student>" +
                               "<Name>Johnny</Name>" +
                               "<Age>16</Age>" +
                           "</Student>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialize_Null()
        {
            new XMLSerializer().Serialize(null);
        }


        [TestMethod]
        public void Serialize_Array_Of_Ints()
        {
            var serializer = new XMLSerializer();

            var ints = new[] { 100, 50 };

            var result = serializer.Serialize(ints);

            Assert.AreEqual("<array><num>100</num><num>50</num></array>", result, 
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Longs()
        {
            var serializer = new XMLSerializer();

            var longs = new[] { 100L, 50L };

            var result = serializer.Serialize(longs);

            Assert.AreEqual("<array><num>100</num><num>50</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Floats()
        {
            var serializer = new XMLSerializer();

            var floats = new[] { 100.5F, 50.5939F };

            var result = serializer.Serialize(floats);

            Assert.AreEqual("<array><num>100.5</num><num>50.5939</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Doubles()
        {
            var serializer = new XMLSerializer();

            var doubles = new[] { 100.5, 50.833 };

            var result = serializer.Serialize(doubles);

            Assert.AreEqual("<array><num>100.5</num><num>50.833</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Chars()
        {
            var serializer = new XMLSerializer();

            var chars = new[] { 'a', 'b', 'c' };

            var result = serializer.Serialize(chars);

            Assert.AreEqual("<array><char>a</char><char>b</char><char>c</char></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Strings()
        {
            var serializer = new XMLSerializer();

            var strings = new[] { "Hola", "Hello, world!", "", " " };

            var result = serializer.Serialize(strings);

            Assert.AreEqual("<array><string>Hola</string><string>Hello, world!</string>" +
                            "<string></string><string> </string></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Bools()
        {
            var serializer = new XMLSerializer();

            var bools = new[] { true, false, false, true, true };

            var result = serializer.Serialize(bools);

            Assert.AreEqual("<array><bool>True</bool><bool>False</bool><bool>False</bool><bool>True</bool>" +
                            "<bool>True</bool></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Dates()
        {
            var serializer = new XMLSerializer();

            var dates = new[]
            {
                new DateTime(2000, 12, 5),
                new DateTime(2015, 2, 2, 8, 5, 10),
                new DateTime(2003, 10, 1, 11, 11, 11),
                new DateTime(1900, 7, 12), 
            };

            var result = serializer.Serialize(dates);

            var expected = "<array>" + 

                           "<date>" + 
                           "<year>2000</year><month>12</month><day>5</day><hour>0" +
                           "</hour><minute>0</minute><second>0</second>" +
                           "</date>" +

                           "<date>" + 
                           "<year>2015</year><month>2</month><day>2</day>" +
                           "<hour>8</hour><minute>5</minute><second>10</second>" +
                           "</date>" +

                           "<date>" + 
                           "<year>2003</year><month>10</month><day>1</day>" +
                           "<hour>11</hour><minute>11</minute><second>11</second>" +
                           "</date>" +

                           "<date>" +
                           "<year>1900</year><month>7</month><day>12</day>" +
                           "<hour>0</hour><minute>0</minute><second>0</second>" +
                           "</date>" +

                           "</array>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Classes()
        {
            var serializer = new XMLSerializer();

            var people = new[] {
                new Person
                {
                    Id = 100,
                },
                new Person(),
                new Person
                {
                    Id = 200,
                    FirstName = "Lolis"
                }
            };

            var result = serializer.Serialize(people);

            var expected = "<array>" +

                           "<Person>" +
                           "<Id>100</Id><FirstName></FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>"+'\0'+"</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "<Person>" +
                           "<Id>0</Id><FirstName></FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>" + '\0' + "</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "<Person>" +
                           "<Id>200</Id><FirstName>Lolis</FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>" + '\0' + "</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "</array>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Classes_With_Class_Properties()
        {
            var serializer = new XMLSerializer();

            var players = new[] {
                new Player(),
                new Player
                {
                    Username = "Wupa9",
                    Level = 100,
                    Thievery = new Skill
                    {
                        Level = 99,
                        Exp = 99999
                    }
                },
                new Player
                {
                    Username = "Dezy",
                    Level = 74,
                    Thievery = null
                },
                new Player
                {
                    Username = "Lolo",
                    Level = 50,
                    Thievery = new Skill
                    {
                        Level = 50,
                        Exp = 53599
                    }
                }
            };

            var result = serializer.Serialize(players);

            var expected = "<array>" +

                          "<Player>" +
                                "<Username></Username>" +
                                "<Level>0</Level>" +
                                "<Thievery>" +
                                "</Thievery>" +
                            "</Player>" +

                          "<Player>" +
                                "<Username>Wupa9</Username>" +
                                "<Level>100</Level>" +
                                "<Thievery>" +
                                    "<Level>99</Level>" +
                                    "<Exp>99999</Exp>" +
                                "</Thievery>" +
                            "</Player>" +

                            "<Player>" +
                                "<Username>Dezy</Username>" +
                                "<Level>74</Level>" +
                                "<Thievery>" +
                                "</Thievery>" +
                            "</Player>" +

                            "<Player>" +
                                "<Username>Lolo</Username>" +
                                "<Level>50</Level>" +
                                "<Thievery>" +
                                    "<Level>50</Level>" +
                                    "<Exp>53599</Exp>" +
                                "</Thievery>" +
                            "</Player>" +


                           "</array>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        private void Serialize<T>(IEnumerable<T> testValues, string tag)
        {
            var serializer = new XMLSerializer();

            foreach (var testVal in testValues)
            {
                var result = serializer.Serialize(testVal);

                Assert.AreEqual("<"+tag+">" + testVal + "</"+tag+">", result, 
                                "Should return the correct xml representation.");
            }
        }


    }
}
