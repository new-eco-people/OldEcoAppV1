import * as moment from 'moment';
import * as _ from 'lodash';

export class AppProblem {
    _: any;
    timeAtTheMoment: any

    data = {
        eco: {
          e: 'Environment',
          c: 'Community',
          o: 'Organization'
        },
        ico: {
          i: 'Individual',
          c: 'Community',
          o: 'Organization'
        },
        unGoals: {
          tag: '',
          name: ''
        }
      };

    constructor(data?: any) {
      this.props = data;
      this.timeAtTheMoment = moment();

    }

    set props(data: any) {
      if (data) {
        const problem = data.problem;
        this._ = {..._.omit(data, 'problem'), ...problem};
      }
    }

    humanTime() {
        // return moment(this._.dateCreated).format('ddd DD MMMM YYYY');
        // return moment(this._.dateCreated).fromNow();
        return moment.duration(this.timeAtTheMoment.diff(moment(this._.dateCreated))).humanize() + ' ago';
    }

    getEco() {
        // console.log(Object.keys(this.data.ico));
        return this._.eco ? this.data.eco[this._.eco] : '';
    }

    getIco() {
        const flag = this._.ico ? this._.ico : '';

        if (Object.keys(this.data.ico).indexOf(flag) > -1) {
          // console.log(this._.icoOther);
          const icoOther: string =  this._.icoOther ? this._.icoOther : '';
          return icoOther.length ? this.data.ico[flag] + ' - ' + icoOther : this.data.ico[flag];
          // return this.data.ico[flag];
        }

        return '';
    }

    getUnGoals(flag: string) {
      // return flag ? flag.length : 2000;
      const data: string[] = [];
      const names: string[] = [];
      if (this._.ecoUn && this._.ecoUn.length >= 2) {
        // data.push(this._.ecoUn[0].toUpperCase() + this._.ecoUn[1].toLowerCase());
        names.push(this._.ecoUn);

        // const v: string = this._.ecoUn;
        // this._.ecoUn = v.toLowerCase();
      }

      if (this._.ecoUnOther && this._.ecoUnOther.length >= 2) {
        data.push(this._.ecoUnOther[0].toUpperCase() + this._.ecoUnOther[1].toLowerCase());
        names.push(this._.ecoUnOther);
      }

      this.data.unGoals.tag = data.join(' - ');
      this.data.unGoals.name = names.join(' - ');

      return this.data.unGoals.name;
    }

    getIcoTag() {
      const flag = this._.ico ? this._.ico : '';
      if (Object.keys(this.data.ico).indexOf(flag) > -1) {
        return flag.toUpperCase();
      }

      return flag.length >= 2 ? flag[0].toUpperCase() + flag[1].toLowerCase() : '';

    }

    replace() {
      // console.log(this._.ecoUn);
      // const v: string = this._.ecoUn;
      // if (v) { return v.replace(/ /g, '_').toLowerCase(); }

      return this._.ecoUn.replace(/ /g, '_').toLowerCase();
      
    }

    fullName() {
      if (!this._.user || !this._.user.personalDetail) { return '';}

      // console.log(this._.user);

      return this._.user.personalDetail.firstName + ' ' + this._.user.personalDetail.lastName[0] + '.';
    }
}
