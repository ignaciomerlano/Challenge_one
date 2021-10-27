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
    public class SendFinishReservationCommandHandlerTest
    {
        private Mock<IUpdateReservationQueue> _updateReservationQueue;
        private Mock<IMediator> _mediator;

        [Fact]
        public async Task GivenReservationId_WhenReservationIsFinished_ThenItShouldBeRetrievedAndSentToQueue()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var slot = new Slot { Id = 1 };
            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-2), Slot = slot };
            var appSettingPrieceHour = new AppSetting { AppKey = "price.hour", AppValue = "3" };
            var appSettingPrieceDay = new AppSetting { AppKey = "price.day", AppValue = "57" };
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            _mediator.Setup(x => x.Send(It.IsAny<GetSlotByIdInternalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slot);
            _mediator.Setup(x => x.Send(It.IsAny<SendUpdateSlotCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Send(It.Is<GetAppSettingValueQuery>(y => y.AppKey == "price.hour"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceHour);
            _mediator.Setup(y => y.Send(It.Is<GetAppSettingValueQuery>(x => x.AppKey == "price.day"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceDay);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            _mediator.Verify(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _updateReservationQueue.Verify(x => x.SendUpdateReservation(reservation), Times.Once);
        }

        [Fact]
        public async Task GivenAReservationTimeLessThan3Hs_WhenReservationIsFinished_CostShoulBe3()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();
            
            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var slot = new Slot { Id = 1 };
            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-2), Slot = slot };
            var appSettingPrieceHour = new AppSetting { AppKey = "price.hour", AppValue = "3" };
            var appSettingPrieceDay = new AppSetting { AppKey = "price.day", AppValue = "57" };
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            _mediator.Setup(x => x.Send(It.IsAny<GetSlotByIdInternalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slot);
            _mediator.Setup(x => x.Send(It.IsAny<SendUpdateSlotCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Send(It.Is<GetAppSettingValueQuery>(y => y.AppKey == "price.hour"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceHour);
            _mediator.Setup(y => y.Send(It.Is<GetAppSettingValueQuery>(x => x.AppKey == "price.day"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceDay);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(3);
        }

        [Fact]
        public async Task GivenAReservationTimeEqual5Hs_WhenReservationIsFinished_CostShoulBe9()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var slot = new Slot { Id = 1 };
            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-5), Slot = slot };
            var appSettingPrieceHour = new AppSetting { AppKey = "price.hour", AppValue = "3" };
            var appSettingPrieceDay = new AppSetting { AppKey = "price.day", AppValue = "57" };
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            _mediator.Setup(x => x.Send(It.IsAny<GetSlotByIdInternalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slot);
            _mediator.Setup(x => x.Send(It.IsAny<SendUpdateSlotCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Send(It.Is<GetAppSettingValueQuery>(y => y.AppKey == "price.hour"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceHour);
            _mediator.Setup(y => y.Send(It.Is<GetAppSettingValueQuery>(x => x.AppKey == "price.day"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceDay);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(9);
        }

        [Fact]
        public async Task GivenAReservationTimeEqual9Hs_WhenReservationIsFinished_CostShoulBe57()
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var slot = new Slot { Id = 1 };
            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-9), Slot = slot };
            var appSettingPrieceHour = new AppSetting { AppKey = "price.hour", AppValue = "3" };
            var appSettingPrieceDay = new AppSetting { AppKey = "price.day", AppValue = "57" };
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            _mediator.Setup(x => x.Send(It.IsAny<GetSlotByIdInternalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slot);
            _mediator.Setup(x => x.Send(It.IsAny<SendUpdateSlotCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Send(It.Is<GetAppSettingValueQuery>(y => y.AppKey == "price.hour"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceHour);
            _mediator.Setup(y => y.Send(It.Is<GetAppSettingValueQuery>(x => x.AppKey == "price.day"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceDay);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(57);
        }

        [Fact]
        public async Task GivenAReservationTimeEqual2Days_WhenReservationIsFinished_CostShoulBe114() 
        {
            //Arrange
            _mediator = new Mock<IMediator>();
            _updateReservationQueue = new Mock<IUpdateReservationQueue>();

            var command = new SendFinishReservationCommand(It.IsAny<Guid>());
            var _sut = new SendFinishReservationCommandHandler(_updateReservationQueue.Object, _mediator.Object);
            var slot = new Slot { Id = 1 };
            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-47), Slot = slot };
            var appSettingPrieceHour = new AppSetting { AppKey = "price.hour", AppValue = "3" };
            var appSettingPrieceDay = new AppSetting { AppKey = "price.day", AppValue = "57" };
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByReservationIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);
            _mediator.Setup(x => x.Send(It.IsAny<GetSlotByIdInternalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(slot);
            _mediator.Setup(x => x.Send(It.IsAny<SendUpdateSlotCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Send(It.Is<GetAppSettingValueQuery>(y => y.AppKey == "price.hour"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceHour);
            _mediator.Setup(y => y.Send(It.Is<GetAppSettingValueQuery>(x => x.AppKey == "price.day"), It.IsAny<CancellationToken>())).ReturnsAsync(appSettingPrieceDay);

            //Act
            reservation = await _sut.Handle(command, new CancellationToken());

            //Assert
            reservation.Cost.ShouldBe(114);
        }
    }
}
