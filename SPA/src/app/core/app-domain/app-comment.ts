import * as moment from 'moment';

export class AppComment {
    dateCreated: Date;
    message: string;
    user: {
        email: string;
        id: string;
        personalDetail: {
            firstName: string;
            lastName: string;
        }
    };

    timeAtTheMoment: any;


    constructor(data: any) {
        Object.assign(this, data);
        this.timeAtTheMoment = moment();
    }

    fullName() {
        
        if(this.user.personalDetail){
            return this.user.personalDetail.firstName + ' ' + this.user.personalDetail.firstName[0] + '.'
        }

        return this.censorEmail(this.user.email);
    }

    private censorWordEmail(str: string) {
        return str[0] + "*".repeat(str.length - 2) + str.slice(-1);
    }

    private censorWordName(str: string) {
        return str[0] + "***" + str[str.length-1];
    }

    private censorEmail(email: string){
        var arr = email.split("@");
        return this.censorWordName(arr[0]) + "@" + this.censorWordEmail(arr[1])
    }

    humanTime() {
        // return moment(this._.dateCreated).format('ddd DD MMMM YYYY');
        return moment.duration(this.timeAtTheMoment.diff(moment(this.dateCreated))).humanize() + ' ago';
            // return moment(this.dateCreated)
    }

}


// dateCreated: "2019-03-25T00:44:40.5389174"
// message: "This should be at the top"
// user:
// email: "booboo@gmail.com"
// id: "4af191ed-a364-4c79-3147-08d6b09d3c26"
// personalDetail: null