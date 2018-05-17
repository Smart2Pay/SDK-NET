using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.SampleNotification.NetCore.Models
{
    public class NotificationCallback : INotificationCallback
    {
        private readonly ILogger _logger;

        public NotificationCallback(ILogger<NotificationCallback> logger)
        {
            _logger = logger;
        }

        public async Task<bool> CardPaymentNotificationCallbackAsync(ApiCardPaymentResponse cardPaymentNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> CardPayoutNotificationCallbackAsync(ApiCardPayoutResponse cardPayoutNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> DisputeNotificationCallbackAsync(ApiDisputeResponse disputeNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> InvalidFormatNotificationCallbackAsync(InvalidFormatNotification invalidFormatNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> AlternativePaymentNotificationCallbackAsync(ApiAlternativePaymentResponse alternativePaymentNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> PreapprovalNotificationCallbackAsync(ApiPreapprovalResponse preapprovalNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> RefundNotificationCallbackAsync(ApiRefundResponse refundNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> UnknownTypeNotificationCallbackAsync(UnknownTypeNotification unknownTypeNotification)
        {
            try
            {
                //simulate asynchronous work, here you will write your own business logic
                await Task.Delay(50);

                //signal operation success by returning true
                return true;
            }
            catch (Exception ex)
            {
                // log the exception
                _logger.LogError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }
    }
}
