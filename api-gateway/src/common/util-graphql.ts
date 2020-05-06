import { ApolloError } from "apollo-server-core";
import { MutationError } from '../graphql'

export class MutationResponse {
    
    private constructor (

        public success: boolean,

        public error?: MutationError,

    ) {
     

    } 
    
    static succes<V> (value: object) {
        const response = new this(true)

        return Object.assign(response, value)

    }

   

    static failure(error: MutationError){
        return new this(false, error)
    }

}