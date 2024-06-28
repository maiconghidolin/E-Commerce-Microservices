import { Module } from "@nestjs/common";
import { UserProducerService } from "./user-exchange.producer";

@Module({
  providers: [UserProducerService],
  exports: [UserProducerService],
})
export class QueueModule {}
