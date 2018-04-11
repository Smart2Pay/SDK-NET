using System;
using System.Collections.Generic;
using System.Text;

namespace S2p.RestClient.Sdk.Entities
{
    public static class CardPaymentStatusDefinition
    {
        public const byte Open = 1;
        public const byte Success = 2;
        public const byte Cancelled = 3;
        public const byte Failed = 4;
        public const byte Expired = 5;
        public const byte PendingOnProvider = 7;
        public const byte Authorized = 9;
        public const byte Captured = 11;
        public const byte CaptureRequested = 13;
        public const byte Exception = 14;
        public const byte CancelRequested = 15;
        public const byte Reversed = 16;
        public const byte Disputed = 19;
        public const byte PartiallyRefunded = 21;
        public const byte Refunded = 22;
        public const byte DisputeWon = 23;
        public const byte DisputeLost = 24;
        public const byte Paid = 25;
        public const byte Chargedback = 26;
        public const byte PendingChallengeConfirmation = 30;
        public const byte QueuedForCapturing = 33;
        public const byte QueuedForCanceling = 34;
        public const byte PartiallyCaptured = 35;
    }
}
