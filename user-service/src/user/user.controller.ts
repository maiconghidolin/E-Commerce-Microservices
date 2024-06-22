import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  BadRequestException,
  UseGuards,
} from "@nestjs/common";
import { UserService } from "./user.service";
import { CreateUserDto } from "./dto/create-user.dto";
import { UpdateUserDto } from "./dto/update-user.dto";
import { AuthGuard } from "src/auth/auth.guard";
import { Roles } from "src/auth/roles.decorator";
import { Role } from "src/auth/role.enum";
import { RolesGuard } from "src/auth/roles.guard";

@Controller("users")
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Post()
  async create(@Body() createUserDto: CreateUserDto) {
    const emailAlreadyRegistered = await this.userService.emailAlreadyExists(
      createUserDto.email,
    );

    if (emailAlreadyRegistered)
      throw new BadRequestException("Email already registered");

    return this.userService.create(createUserDto);
  }

  @Get()
  @Roles(Role.Admin)
  @UseGuards(AuthGuard, RolesGuard)
  findAll() {
    return this.userService.findAll();
  }

  @Get(":id")
  @Roles(Role.Admin)
  @UseGuards(AuthGuard, RolesGuard)
  findOne(@Param("id") id: string) {
    return this.userService.findOne(id);
  }

  @Patch(":id")
  @UseGuards(AuthGuard)
  update(@Param("id") id: string, @Body() updateUserDto: UpdateUserDto) {
    return this.userService.update(id, updateUserDto);
  }

  @Delete(":id")
  @Roles(Role.Admin)
  @UseGuards(AuthGuard, RolesGuard)
  remove(@Param("id") id: string) {
    return this.userService.remove(id);
  }
}
