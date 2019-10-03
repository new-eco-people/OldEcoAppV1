import { tassign } from 'tassign';
import { ProblemActionConstant } from './problem-action-constants';
import { ProblemService } from 'src/app/shared/_services/problem.service';


export interface IProblemAppState {

    // Auth Service
    searchFilter: any;

    // Simple comment or idea checker
    isComment: boolean

}

export const PROBLEM_INITIAL_STATE: IProblemAppState = {

    // Auth Service
    searchFilter: {},

    // Simple comment or idea checker
    isComment: true,
};



export function problemReducer(state: IProblemAppState = PROBLEM_INITIAL_STATE, action): IProblemAppState {

    const obj = new ProblemActions(state, action);

    switch (action.type) {

        // Auth Service
        case ProblemActionConstant.SEARCH_FOR_PROBLEM: return obj.searchProblems();
        case ProblemActionConstant.TOGGLE_COMMENT_IDEA: return obj.toggleIsComment();


        default: return state;
    }
}

export class ProblemActions {

    constructor(private state: IProblemAppState, private action) {
    }

    // Auth Service
    searchProblems() {
        // console.log(this.action.searchFilter);
        return tassign(this.state, { searchFilter: this.action.searchFilter });
    }

    toggleIsComment() {
        const isComment = !this.state.isComment;

        return tassign(this.state, { isComment });
    }


}
