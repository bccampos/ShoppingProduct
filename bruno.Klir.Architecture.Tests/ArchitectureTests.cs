using FluentAssertions;
using NetArchTest.Rules;

namespace bruno.Klir.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "bruno.Klir.Domain";
        private const string ApplicationNamespace = "bruno.Klir.Application";
        private const string InfrastructureNamespace = "bruno.Klir.Infrastructure";
        private const string ContractsNamespace = "bruno.Klir.Contracts";
        private const string WebApiNamespace = "bruno.Klir.WebApi";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange 
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                ContractsNamespace,
                WebApiNamespace
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange 
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                WebApiNamespace,
                InfrastructureNamespace,                
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange 
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                WebApiNamespace
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}
