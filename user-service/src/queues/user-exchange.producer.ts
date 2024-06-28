import { HttpException, HttpStatus, Injectable, Logger } from "@nestjs/common";
import amqp, { ChannelWrapper } from "amqp-connection-manager";
import { Channel } from "amqplib";

@Injectable()
export class UserProducerService {
  private _channel: ChannelWrapper;
  private _exchangeName = "user-exchange";

  constructor() {
    const connection = amqp.connect(["amqp://localhost"]);

    this._channel = connection.createChannel({
      setup: (channel: Channel) => {
        return channel.assertExchange(this._exchangeName, "topic", {
          durable: true,
        });
      },
    });
  }

  async publish(routingKey: string, data: any) {
    try {
      await this._channel.publish(
        this._exchangeName,
        routingKey,
        Buffer.from(JSON.stringify(data)),
        {
          persistent: true,
        },
      );

      Logger.log("Event sent to user exchange");
    } catch (error) {
      throw new HttpException(
        "Error sending event to user exchange",
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
