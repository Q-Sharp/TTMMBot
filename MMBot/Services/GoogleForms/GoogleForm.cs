﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MMBot.Services.GoogleForms
{
    public class GoogleForm
    {
        /// <summary>
        /// Document Name of your Google Form
        /// </summary>
        public string FormDocName { get; set; }

        /// <summary>
        /// Form ID of your Google Form
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Title of your Google Form
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of your Google Form
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of Question Fields in your Google Form
        /// </summary>
        public List<GoogleFormField> QuestionFieldList { get; set; }
    }
}