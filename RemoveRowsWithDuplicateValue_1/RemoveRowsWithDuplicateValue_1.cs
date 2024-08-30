/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

30/08/2024	1.0.0.1		MFA, Skyline	Initial version
****************************************************************************
*/

namespace RemoveRowsWithDuplicateValue_1
{
    using System;
    using System.Collections.Generic;
    using Skyline.DataMiner.Analytics.GenericInterface;

    [GQIMetaData(Name = "RemoveRowsWithDuplicateValue")]
    public sealed class RemoveRowsWithDuplicateValue : IGQIRowOperator, IGQIInputArguments
    {
        private readonly GQIColumnDropdownArgument _valueColumnArg = new GQIColumnDropdownArgument("Value Column") { IsRequired = true };
        private HashSet<string> _knownValues = new HashSet<string>();
        private GQIColumn _column;

        public GQIArgument[] GetInputArguments()
        {
            return new GQIArgument[] { _valueColumnArg };
        }

        public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
        {
            _column = args.GetArgumentValue(_valueColumnArg);
            return new OnArgumentsProcessedOutputArgs();
        }

        public void HandleRow(GQIEditableRow row)
        {
            string value = row.GetDisplayValue(_column);
            if (_knownValues.Contains(value))
            {
                row.Delete();
            }
            else
            {
                _knownValues.Add(value);
            }
        }
    }
}
