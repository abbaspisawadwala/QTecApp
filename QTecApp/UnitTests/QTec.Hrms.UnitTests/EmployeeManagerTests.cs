﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QTec.Hrms.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Practices.Unity;

    using QTec.Hrms.Business.Contracts;
    using QTec.Hrms.Business.CustomExceptions;
    using QTec.Hrms.Business.Personal;
    using QTec.Hrms.DataTier;
    using QTec.Hrms.DataTier.Contracts;
    using QTec.Hrms.DataTier.Repositories;
    using QTec.Hrms.Models;

    using Telerik.JustMock;

    [TestClass]
    public class EmployeeManagerTests
    {
        private IUnityContainer container;
        [TestInitialize]
        public void Setup()
        {

            container = BuildUnityContainer();
            


        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            

            container.RegisterInstance(typeof(RepositoryFactories), new RepositoryFactories());
            container.RegisterType<IRepositoryProvider, RepositoryProvider>();
            container.RegisterType<IQTecUnitOfWork, QTecUnitOfWork>();

            return container;
        }
        [TestMethod]
        public void DesignationsList_Cannot_Be_Null()
        {
            // ARRANGE
            var uow = Mock.Create<IQTecUnitOfWork>();
            var expectedDesignations = new List<Designation>
                                           {
                                               new Designation { Id = 1, Name = "HR" },
                                               new Designation { Id = 2, Name = "Finance" }
                                           };
            var queryableDesignations = expectedDesignations.AsQueryable();
            Mock.Arrange(() => uow.DesignationRepository.GetAll()).Returns(queryableDesignations);
            
            var classUnderTest = new EmployeeManager(uow);

            // ACT
            var returnValues = classUnderTest.GetDesignations();

            // ASSERT
            Assert.IsNotNull(returnValues);


        }

        [TestMethod]
        public void Assert_False_If_Email_Does_Not_Exists()
        {
            //var uow = Mock.Create<IQTecUnitOfWork>();
            //Mock.Arrange(() => uow.EmployeeRepository.IsEmailDuplicate("afzal@quipment.in")).CallOriginal();
            var svcLocator = new UnityServiceLocator(container);
            var uow = svcLocator.GetInstance<IQTecUnitOfWork>();
               
            var classunderTest = new EmployeeManager(uow);

            const bool ExpectedValue = false;
            var actualValue = classunderTest.IsEmailUnique("afzal@quipment.in");

            Assert.AreEqual(ExpectedValue,actualValue);
        }

        [TestMethod]
        public void Valid_Employee_Should_Be_Returned_When_Requested()
        {
            // ARRANGE
            var expectedEmployee = new Employee
                                       {
                                           EmployeeId = 1, 
                                           DateOfBirth = new DateTime(1998, 2, 3), 
                                           DesignationId = 2
                                       };

            var uow = Mock.Create<IQTecUnitOfWork>(Behavior.Loose);
            Mock.Arrange(() => uow.EmployeeRepository.GetById(Arg.AnyInt)).Returns(expectedEmployee);

            // ACT
            var classunderTest = new EmployeeManager(uow);

            // ASSERT
            var actualEmployee = classunderTest.GetEmployeeById(1);

            Assert.AreEqual(expectedEmployee, actualEmployee);


        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmployeeIdException))]
        public void Exception_Must_Be_Thrown_If_EmployeeId_Zero()
        {
            var uow = Mock.Create<IQTecUnitOfWork>(Behavior.Loose);
            // ACT
            var classunderTest = new EmployeeManager(uow);

            // ASSERT
            var actualEmployee = classunderTest.GetEmployeeById(0);

            Assert.IsNull(actualEmployee);
        }
    }
}
