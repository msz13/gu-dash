
/** ------------------------------------------------------
 * THIS FILE WAS AUTOMATICALLY GENERATED (DO NOT MODIFY)
 * -------------------------------------------------------
 */

/* tslint:disable */
export interface MutationResponse {
    success?: boolean;
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

export class CreateCompetenceDTO {
    name: string;
    description?: string;
    isRequired: boolean;
}

export class CreateCompetenceResponse implements MutationResponse {
    success?: boolean;
    competence?: Competence;
}

export abstract class IMutation {
    abstract createCompetence(createCompetence?: CreateCompetenceDTO): CreateCompetenceResponse | Promise<CreateCompetenceResponse>;
}

export abstract class IQuery {
    abstract competences(): Competence[] | Promise<Competence[]>;
}
