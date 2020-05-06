
/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
export class CreateCompetenceDTO {
    name: string;
    description?: string;
    isRequired?: boolean;
}

export class Competence {
    competenceId?: string;
    name: string;
    description?: string;
    isRequired: boolean;
    numberOfActiveGoals: number;
    numberOfHoldedGoals: number;
    numberOfDoneGoals: number;
}

export class CreateCompetenceResponse {
    success?: boolean;
    error?: MutationError;
    competence?: Competence;
}

export abstract class IMutation {
    abstract createCompetence(createCompetenceInput?: CreateCompetenceDTO): CreateCompetenceResponse | Promise<CreateCompetenceResponse>;

    abstract registerUser(registerUserInput?: RegisterUserInput): RegisterUserResponse | Promise<RegisterUserResponse>;
}

export class MutationError {
    code: string;
    message: string;
}

export abstract class IQuery {
    abstract competences(): Competence[] | Promise<Competence[]>;
}

export class RegisterUserInput {
    userId: string;
    userSettings?: UserSettings;
}

export class RegisterUserResponse {
    success?: boolean;
}

export class UserSettings {
    timezone: string;
    firstDayOfWeek?: number;
    email?: string;
}
