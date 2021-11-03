import Vue from 'vue';
import moment from 'moment';
import Helpers from './utils/helpers';
import { DocumentType, UserRole } from './utils/enums';

Vue.filter('bTableColumnValue', Helpers.ToBTableColumnValue)
Vue.filter('date', (value: string) => value ? moment(value).format('L') : Helpers.Filters.EmptyText())
Vue.filter('time', (value: string) => value ? moment(value).format('LT') : Helpers.Filters.EmptyText())
Vue.filter('bool', (value: boolean) => value ? 'Sí' : 'No')
Vue.filter('timeAgo', (value: string) => value ? `${Helpers.Filters.FromNow(value)}` : Helpers.Filters.EmptyText())
Vue.filter('empty', (value: string) => value || Helpers.Filters.EmptyText())
Vue.filter('dots', (value: string, count: number = 20) => value ? `${Helpers.Filters.Dots(value, count)}` : Helpers.Filters.EmptyText())
Vue.filter('capitalize', (value: string) => value ? value.toUpperCase() : value);
Vue.filter("cedula", (value: string) => !!value ? Helpers.Filters.Cedula(value) : Helpers.Filters.EmptyText());
Vue.filter("phone", (value: string) => !!value ? Helpers.Filters.Phone(value) : Helpers.Filters.EmptyText());
Vue.filter("documentType", (value: DocumentType) => value in DocumentType ? Helpers.Filters.DocumentType(value) : Helpers.Filters.EmptyText());
Vue.filter("userRole", (value: UserRole) => value in UserRole ? Helpers.Filters.UserRole(value) : Helpers.Filters.EmptyText());
Vue.filter("documentTypeValue", (value: string, documentType: DocumentType) => {
    if (!value)
        return Helpers.Filters.EmptyText();
    return documentType === DocumentType.ID ? Helpers.Filters.Cedula(value) : value
});
