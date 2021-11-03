import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Helpers from "@/core/utils/helpers";
import moment from 'moment';

@Component
export class CoreMixin extends Vue {
    created(){
        this.$validator.localize('es');
    }
    public yearSelected = 0;
    public get yearRange() {
        return [-100,1000];
    }
    get meses() {
        return Helpers.MonthNames;
    }
    get dias() {
        return Helpers.DayNames;
    }
    getFormatDate(date: Date | string) {
        return `DateTime.Parse("${moment(date).format()}")`;
    }

    getDateTimeOffsetFormatDate(date: Date | string) {
        return `DateTimeOffset.Parse("${moment(date).format()}")`;
    }
    formatDate(date: Date) {
        var mm = date.getMonth() + 1;
        var dd = date.getDate();
        var yyyy = date.getFullYear();
        var format = dd + '/' + mm + '/' + yyyy;
        return format;
    }
    operationSuccess(message: string = "Operación exitosa!", duration = 2000) {
        this.$buefy.toast.open({
          message: message,
          type: "is-success",
          duration: duration,
          position: "is-bottom-right"
        });
    }

    operationFailed(message: string = "Operación fallída!", duration = 4000) {
        this.$buefy.toast.open({
            message: message,
            type: 'is-danger',
            position: "is-top-right",
            duration: duration
        });
    }
    operationNoAllowed(message: string = "Operación no permitida!", duration = 4000) {
        this.$buefy.toast.open({
            message: message,
            type: 'is-warning',
            duration: duration,
            position: "is-bottom",
        });
    }

    operationFailedErrors(errors: string[], queue = false, duration = 4000, type = "is-danger") {
        errors.forEach(e => this.$buefy.toast.open({
            message: e,
            duration: duration,
            queue: queue,
            type: type,
            position: "is-top-right"
        }))
    }

    copy(obj: any) {
        return JSON.parse(JSON.stringify(obj));
    }

    map<T>(from: any) {
        return Helpers.MapSimpleObject<T>(from, {});
    }

    equals(from: any, to: any): boolean {
        let result = false;
        for (let key in from) {
            if (to.hasOwnProperty(key)) {
                if (to[key] === from[key])
                    result = true;
                else {
                    result = false;
                    break;
                }
            } else
                result = false;
        }

        return result;
    }
}
