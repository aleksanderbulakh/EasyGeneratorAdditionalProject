define([], function () {
    return {

        DEFAULT_COURSE_NAME: 'New course',

        SINGLE_SELECT_QUESTION_TYPE: 'single',
        MULTIPLE_SELECT_QUESTION_TYPE: 'multiple',
        SINGLE_SELECT_IMAGE_QUESTION_TYPE: 'single_image',

        QUESTION_TYPE_RADIO: 'radio',
        QUESTION_TYPE_CHECKBOX: 'checkbox',

        DATA_TYPES: {
            OBJECT: 'object',
            BOOL: 'boolean',
            NUMBER: 'number'
        },

        VIEWS_ANSWER_TYPES: {
            'single': 'radio',
            'multiple': 'checkbox',
            'single_image': 'radio'
        },
        MODELS_NAMES: {
            COURSE: 'Course',
            SECTION: 'Section',
            QUESTION: 'Question',
            ANSWER: 'Answer'
        },
        MESSAGES: {
            DATA_IS_NOT_FOUND: 'Data is not found.',
            TITLE_CHANGED: 'Title has been changed.',
            INVALID_DATA: 'Invalid data'
        },
        MESSAGES_STATE: {
            SUCCESS: 'Seccess',
            Error: 'Error'
        },
        EVENTS: {
            QUESTION_DELETE: 'question:deleted',
            QUESTION_MODIFIED: 'question:modified',
            SECTION_DELETED: 'section:deleted',
            ANSWER_DELETED: 'answer:deleted',

        }
    }
})