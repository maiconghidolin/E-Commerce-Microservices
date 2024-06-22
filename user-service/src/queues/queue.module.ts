import { Module } from "@nestjs/common";
import { ProducerService } from "./queue.producer";

@Module({
  providers: [ProducerService],
  exports: [ProducerService],
})
export class QueueModule {}
