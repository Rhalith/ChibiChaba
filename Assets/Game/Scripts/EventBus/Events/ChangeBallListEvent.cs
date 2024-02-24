using Ball;

namespace EventBus.Events
{
    public struct ChangeBallListEvent
    {
        public bool IsAdd;
        public BallController Ball;
    }
}