using System;
using System.Diagnostics;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.SampleNotification.WebApi.Models
{
    public class NotificationCallback : INotificationCallback
    {
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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }

        public async Task<bool> PaymentNotificationCallbackAsync(ApiPaymentResponse paymentNotification)
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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

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
                Trace.TraceError(ex.Message);

                //signal operation failure by returning false
                return false;
            }
        }
    }
}