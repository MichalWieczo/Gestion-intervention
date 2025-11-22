using System;
using System.ComponentModel;

namespace Gestion_intervention.Model.Gestion_intervention.Entities
{
    public class Intervention : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public enum Category { EM, CM, MR }

        // 🟣 Nouveaux ENUMS
        public enum ProblemCode 
        {
            Unknown,
            P101_Safety_system_failure,
            P102_Other_safety_risk,
            P103_Contamination_risk,
            P104_Quality_other_risk,
            P105_Blockage_due_to_equipment,
            P106_Blockage_due_to_product,
            P107_Blockage_due_to_waste,
            P108_Blockage_completely_unusable,
            P109_Blockage_reduced_speed,
            P110_Leakage,
            P111_Current_problem,
            P112_Pressure_problem,
            P113_Rotation_problem,
            P114_Temperature_problem,
            P115_Noise_problem,
            P116_Excessive_vibration,
            P117_Misalignment,
            P118_Weight_measurement,
            P119_Hydraulic_system_malfunction,
            P120_Pneumatic_system_malfunction,
            P121_Vacuum_system_malfunction,
            P122_Water_system_malfunction,
            P123_Other_utilities_failure,
            P124_Alarm,
            P125_Setting_problem,
            P126_Display_problem,
            P127_Abnormal_signal,
            P128_Electrical_short_circuit,
            P129_Electrical_fuse_or_switch,
            P130_Electrical_power_loss,
            P131_Other_mulfunction,
            P132_Something_broken
        }
        public enum CauseCode 
        { 
            Unknown, 
            C101_Blocked_by_product,
            C102_Foreign_object_in_device,
            C103_Foreign_object_in_product,
            C104_Air_leak,
            C105_Loose_connection,
            C106_Overloaded,
            C107_Corrosion,
            C108_Aging,
            C109_Faulty_structure,
            C110_Detached,
            C111_Part_missing,
            C112_Leak_sealing_problems,
            C113_Valve_failure,
            C114_Bearing_failure,
            C115_Gearbox_problems,
            C116_Clutch_problems,
            C117_Chain_and_gear_problems,
            C118_Broken_transmission_belts,
            C119_Broken_hose,
            C120_Broken_conveyor_belts,
            C121_Wear_Cracked_Broken,
            C122_Safety_circuit_problem,
            C123_Defective_sensor,
            C124_Burnt_component_Short_circuit_Open_circuit,
            C125_Failure_in_the_power_supply,
            C126_PLC_error,
            C127_Power_failure,
            C128_Dirt,
            C129_Incorrect_settings_NATO,
            C130_Incorrect_settings_ATO,
            C131_Software_issue,
            C132_Human_error,
            C133_Process_overload,
            C134_Process_variation,
            C135_Emergency_stop_activated,
            C136_Deliberate_damage,
            C137_Has_been_modified,
            C138_No_product_or_packaging_material,
            C139_Product_not_according_to_specifications,
            C140_Packaging_material_not_according_to_specification,
            C141_Incorrect_product_or_packaging_material,
            C142_Weather_problems,
            C143_Installation_problems,
            C144_Other_causes_Specify_in_remarks
        }
        public enum SolutionCode 
        { 
            Unknown, 
            R101_Problem_disappeared_without_action,
            R102_Manually_adjusted,
            R103_Temporarily_repaired,
            R104_Reset_Restarted_Power_interruption,
            R105_Explained_to_operator,
            R106_Cleaned,
            R107_Emptied_Material_removed,
            R108_Settings_adjustment_ATO,
            R109_Settings_adjustment_NATO,
            R110_Calibrated,
            R111_Pressure_released_Drained_Vented,
            R112_Refilled,
            R113_Unclogged,
            R114_Alignment_adjusted,
            R115_Lubricated,
            R116_Reassembled,
            R117_Rewired,
            R118_Only_tightened,
            R119_Replaced_the_entire_functional_component,
            R120_Repaired_using_machining_or_welding,
            R121_Repaired_maintenance_kit_only,
            R122_Adjustment_Specify_in_remarks
        }

        private int _id;
        private string _name;
        private DateTime? _startTime;
        private DateTime? _endTime;
        private Category _category;
        private ProblemCode _problem;
        private CauseCode _cause;
        private SolutionCode _solution;
        private string _description;

        public Intervention(int id, string name, DateTime? startTime, DateTime? endTime, Category categoryType, ProblemCode problem, CauseCode cause, SolutionCode solution, string description)
        {
            Id = id;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            CategoryType = categoryType;
            Problem = problem;
            Cause = cause;
            Solution = solution;
            Description = description;
        }

        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }

        public DateTime? StartTime
        {
            get => _startTime;
            set { _startTime = value; OnPropertyChanged(nameof(StartTime)); OnPropertyChanged(nameof(Duration)); }
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set { _endTime = value; OnPropertyChanged(nameof(EndTime)); OnPropertyChanged(nameof(Duration)); }
        }

        public Category CategoryType
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(CategoryType)); }
        }

        // 🟣 Nouvelles propriétés
        public ProblemCode Problem
        {
            get => _problem;
            set { _problem = value; OnPropertyChanged(nameof(Problem)); }
        }

        public CauseCode Cause
        {
            get => _cause;
            set { _cause = value; OnPropertyChanged(nameof(Cause)); }
        }

        public SolutionCode Solution
        {
            get => _solution;
            set { _solution = value; OnPropertyChanged(nameof(Solution)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public TimeSpan? Duration =>
            (StartTime.HasValue && EndTime.HasValue)
                ? EndTime.Value - StartTime.Value
                : null;

        // === Validation simple ===
        public bool isValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                message = "The intervention name cannot be empty.";
                return false;
            }

            if (EndTime.HasValue && StartTime.HasValue && StartTime.Value >= EndTime.Value)
            {
                message = "The start time must be earlier than the end time.";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
