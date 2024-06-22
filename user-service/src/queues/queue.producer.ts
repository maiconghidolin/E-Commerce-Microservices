import { HttpException, HttpStatus, Injectable, Logger } from "@nestjs/common";
import amqp, { ChannelWrapper } from "amqp-connection-manager";
import { Channel } from "amqplib";

@Injectable()
export class ProducerService {
  private channelWrapper: ChannelWrapper;

  constructor() {
    const connection = amqp.connect(["amqp://localhost"]);

    this.channelWrapper = connection.createChannel({
      setup: (channel: Channel) => {
        return channel.assertQueue("NotificationQueue", { durable: true });
      },
    });
  }

  async addToNotificationQueue(data: any) {
    try {
      await this.channelWrapper.sendToQueue(
        "NotificationQueue",
        Buffer.from(JSON.stringify(data)),
        {
          persistent: true,
        },
      );

      Logger.log("Sent to notification service");
    } catch (error) {
      throw new HttpException(
        "Error sending event to notification service",
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
