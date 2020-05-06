using Xunit;


namespace CommonTests
{
    using GuDash.Common.applicationlayer;
    public class ExampleCmdNotofication : Notification
    {
        public ExampleCmdNotofication() : base() { }

        public void NameIsTaken(string name)
        {
            this.Errors.Add(new Notification.Error("NAME_IS_TAKEN", $"{name} is taken"));
        }
    }
    class ExampleCommand
    {


        public int Age { get; private set; }

        public ExampleCmdNotofication Notification { get; } = new ExampleCmdNotofication();
    }



    public class CommandNotificationsTest
    {
        [Fact]
        public void HasErrors_ReturnTrue()
        {
            var command = new ExampleCommand();

            var notification = command.Notification;

            Assert.False(notification.HasErrors());

        }

        [Fact]
        public void HasErrors_ReturnFalse_And_Error()
        {
            var command = new ExampleCommand();

            var notification = command.Notification;

            notification.Errors.Add(new Notification.Error("EXAMPL", "Example message"));

            Assert.True(command.Notification.HasErrors());

            Assert.Equal("EXAMPL", command.Notification.Errors[0].Code);
        }

        [Fact]
        public void NameIsTaken()
        {
            var command = new ExampleCommand();

            var notification = command.Notification;

            notification.NameIsTaken("Mat");

            Assert.Equal("NAME_IS_TAKEN", notification.Errors[0].Code);

            Assert.Equal("Mat is taken", notification.Errors[0].Message);


        }



    }
}
