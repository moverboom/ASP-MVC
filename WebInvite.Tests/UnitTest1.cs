using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebInviteOpdracht.Models;
using WebInviteOpdracht.Controllers;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.Linq;

namespace WebInvite.Tests {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void FindResponse() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            //act
            target.AddResponse(response);

            //assert
            Assert.AreEqual(response, target.GetResponse(response.Email));
        }
        

        [TestMethod]
        public void AddResponseEmptyList() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            //act and assert
            Assert.AreEqual(true, target.AddResponse(response));
        }

        [TestMethod]
        public void AddResponseFilledList() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };
            GuestResponse response2 = new GuestResponse {
                Name = "Piet",
                Email = "piet@test.nl",
                Phone = "0656934567",
                WillAttend = false
            };
            GuestResponse response3 = new GuestResponse {
                Name = "Klaas",
                Email = "klaas@test.nl",
                Phone = "0625426898",
                WillAttend = true
            };
            GuestResponse response4 = new GuestResponse {
                Name = "Truus",
                Email = "truus@test.nl",
                Phone = "0610873754",
                WillAttend = true
            };

            //act
            target.AddResponse(response2);
            target.AddResponse(response3);
            target.AddResponse(response4);

            //assert
            Assert.AreEqual(true, target.AddResponse(response));
        }

        [TestMethod]
        public void AddResponseExisting() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse response = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            //act
            target.AddResponse(response);

            //assert
            Assert.AreEqual(false, target.AddResponse(response));
        }

        [TestMethod]
        public void AddResponseExistingDifferentAttend() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse firstResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };

            GuestResponse editedAttendResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = false
            };

            //act
            target.AddResponse(firstResponse);

            //assert
            Assert.AreEqual(true, target.AddResponse(editedAttendResponse));
        }

        [TestMethod]
        public void AddResponseExistingDifferentDataRsvpForm() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse firstResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = false
            };

            GuestResponse editedResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0699999999",
                WillAttend = false
            };

            //act
            target.AddResponse(firstResponse);

            //assert
            Assert.AreEqual(false, target.AddResponse(editedResponse));
        }

        [TestMethod]
        public void EditDataKnowEmail() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse firstResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = false
            };

            GuestResponse editedResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0699999999", //Edited field
                WillAttend = false
            };

            //act
            target.AddResponse(firstResponse);

            //assert
            Assert.IsTrue(target.EditResponse(editedResponse));
        }

        [TestMethod]
        public void EditDataUnknowEmail() {
            //arrange
            IRepository target = new GuestResponseRepository();
            GuestResponse firstResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = false
            };

            GuestResponse editedResponse = new GuestResponse {
                Name = "Matthijs",
                Email = "edited@test.nl",
                Phone = "0699999999", //Edited field
                WillAttend = false
            };

            //act
            target.AddResponse(firstResponse);

            //assert
            Assert.IsFalse(target.EditResponse(editedResponse));
        }

        [TestMethod]
        public void ReturnsResponsesView() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(r => r.GetAllResponses()).Returns(new List<GuestResponse> {
                new GuestResponse {
                    Name = "Matthijs Overboom",
                    Email = "test@test.nl",
                    Phone = "0612345678",
                    WillAttend = true
                }
            });
            HomeController controller = new HomeController(mock.Object);

            //act
            ViewResult result = controller.Responses();

            //assert
            Assert.IsInstanceOfType(result.Model, mock.Object.GetAllResponses().ToArray().GetType());
            Assert.AreEqual("Responses", result.ViewName);
        }

        [TestMethod]
        public void ReturnsNoResponsesView() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(r => r.GetAllResponses()).Returns(new List<GuestResponse> {
                //Empty list
            });
            HomeController controller = new HomeController(mock.Object);

            //act
            ViewResult result = controller.Responses();

            //assert
            Assert.IsNull(result.Model);
            Assert.AreEqual("NoResponses", result.ViewName);
        }

        [TestMethod]
        public void ReturnsEditDataView() {
            //arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            GuestResponse returnValue = new GuestResponse {
                Name = "Matthijs Overboom",
                Email = "test@test.nl",
                Phone = "0612345678",
                WillAttend = true
            };
            mock.Setup(r => r.GetResponse(It.IsAny<string>())).Returns(returnValue);
            HomeController controller = new HomeController(mock.Object);

            //act
            ViewResult result = controller.Editdata(mock.Object.GetResponse("test@test.nl"));

            //assert
            Assert.AreEqual("EditData", result.ViewName);
        }
    }
}
