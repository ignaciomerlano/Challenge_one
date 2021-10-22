using Challenge_one.Aplication.Command;
using Challenge_one.Aplication.Queries;
using Challenge_one.Aplication.ReservationQueue;
using Challenge_one.Model;
using Challenge_one.MsSql.ReservationRepository;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestChallenge_one
{
    public class ReservationsUnitTest
    {

        private SendFinishReservationCommandHandler _sut;
        private Mock<IReservationRepository> _reservationRepoMock;
        private Mock<IUpdateReservationQueue> _updateReservationQueue;
        private Mock<IMediator> _mediator;

        [Fact]
        public async Task GivenReservationId_ThenItShouldBeRetrieved()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-2);

            //Act
            await _sut.Handle(command, new CancellationToken());

            //Assert
            _mediator.Verify(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenReservationId_ThenItShouldBeRetrievedAndSentToQueue()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-2);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            _mediator.Verify(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _updateReservationQueue.Verify(x => x.SendUpdateReservation(reservation), Times.Once);
        }

        [Fact]
        public async Task IfReservationTimeIslessThan3Hs_CostShoulBe3()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-2);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(3);
        }

        [Fact]
        public async Task IfReservationTimeIs5Hs_CostShoulBe9()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-5);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(9);
        }

        [Fact]
        public async Task IfReservationTimeIs9Hs_CostShoulBe57()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-9);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(57);
        }

        [Fact]
        public async Task IfReservationTimeIs2Days_CostShoulBe57()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var reservation = new Reservation();
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            reservation.CheckIn = DateTime.Now.AddHours(-47);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(114);
        }
    }
}
