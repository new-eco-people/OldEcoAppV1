import { ProblemService } from 'src/app/shared/_services/problem.service';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ShowPictureComponent } from '../_modals/show-picture/show-picture.component';

@Injectable()
export class ShareProblemFormStructure {

    countries = [];
    states = [];
    file: string;

    eco = [
        {value: 'e', name: 'Environment'},
        {value: 'c', name: 'Community'},
        {value: 'o', name: 'Organization'}
    ];

    icos = [
        {value: 'i', name: 'Individual'},
        {value: 'c', name: 'Community'},
        {value: 'o', name: 'Organization'}
    ];

    ecoUn = [
        {value: 'e', name: 'Environment', unGoals: ['Clean water and Sanitation', 'Affordable and Clean Energy', 'Climate Action', 'Life below water', 'Life on land', 'Responsible Consumption and Production']},
        {value: 'c', name: 'Community', unGoals: ['No poverty', 'Zero hunger', 'Good health and Well being', 'Quality Education', 'Sustainable Cities and Communities', 'Peace, Justice and strong Institutions']},
        {value: 'o', name: 'Organization', unGoals: ['Gender equality', 'Decent work and Economic growth', 'Industry, Innovation and infrastructure', 'Reduce Inequalities', 'Partnerships for the Goals']}
    ];

    ecoUnDisplay: any = { value: '', name: '', unGoals: [] };

    data = {
        countryId: null,
        stateId: null,
        eco: null,
        ecoUn: null,
        ecoUnOther: null,
        ico: null,
        icoOther: null,
        description: null,
        images: [],
        firstName: null,
        lastName: null,
        email: null,
        rememberMe: false,
        addProblemToUser: false,
        addProjectToProblem: false,
        projectLink: null,
        agree: false
    };

    constructor( private problemService: ProblemService, private dialog: MatDialog) {

    }

    setUnGoals() {
        this.ecoUnDisplay = this.ecoUn.find(name => name.value === this.data.eco);
    }

    storeTempFile(event) {
        this.file = event;
    }

    addImage() {
        this.data.images.push(this.file);
        this.file = null;
    }

    removeImage(i: number) {
        this.data.images.splice(i, 1);
    }

    showImage(i: number) {
        this.dialog.open(ShowPictureComponent, {
            data: {
                img: this.data.images[i]
            }
        });
    }


}
