using Challenge_one.Model;
using Challenge_one.MsSql.SlotRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestChallenge_one.Mocks
{
    public static class MockSlotRepository
    {
        public static Mock<ISlotRepository> GetSlots()
        {
            var slots = new List<Slot>
            {
                new Slot
                {
                    SlotId = new Guid(),
                    Number = 1,
                    IsAvailable = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Slot
                {
                    SlotId = new Guid(),
                    Number = 2,
                    IsAvailable = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Slot
                {
                    SlotId = new Guid(),
                    Number = 3,
                    IsAvailable = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            var mockRepo = new Mock<ISlotRepository>();

            mockRepo.Setup(x => x.GetSlots()).ReturnsAsync(slots);

            return mockRepo;
        }
    }
}
