import { ApolloError } from "apollo-server-core";

export class MutationResponse {
    
    constructor (

        public succes: boolean,

        public code: string,

        public message: string,

        public error?: ApolloError

    ) {

    } 

    [key: string]: any

    static success(code: string, message: string, payload: [string, any][]){
        const response = new this(true, code, message)
        payload.forEach(tuple=>{
           return response[tuple[0]]=tuple[1]
        } )

        return response

    }

    static failure(code: string, message: string, error: ApolloError){

    }

}