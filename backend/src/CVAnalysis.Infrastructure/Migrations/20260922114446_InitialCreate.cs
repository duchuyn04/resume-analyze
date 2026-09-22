using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVAnalysis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    actor_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    actor_role = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    action = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    target_entity = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    target_entity_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ip_address = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    details_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "EmailHashRegistry",
                schema: "dbo",
                columns: table => new
                {
                    email_hash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    trial_blocked_until = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHashRegistry", x => x.email_hash)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    role = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    is_email_verified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    email_verified_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CreditLedger",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    analysis_request_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    entry_type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    credit_type = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    balance_before = table.Column<int>(type: "int", nullable: false),
                    balance_after = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditLedger", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CreditLedger_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CreditPackages",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    package_name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    credits_amount = table.Column<int>(type: "int", nullable: false),
                    price_vnd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditPackages", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CreditPackages_Users",
                        column: x => x.created_by_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CVFiles",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    file_size_bytes = table.Column<long>(type: "bigint", nullable: false),
                    page_count = table.Column<int>(type: "int", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    raw_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    extracted_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVFiles", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CVFiles_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "JobDescriptions",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    raw_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    word_count = table.Column<int>(type: "int", nullable: false),
                    min_requirements_met = table.Column<bool>(type: "bit", nullable: false),
                    extracted_skills_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescriptions", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_JobDescriptions_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserBalances",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    free_trial_credits = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    paid_credits = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    held_credits = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalances", x => x.user_id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserBalances_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOrders",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    order_code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    credits_amount = table.Column<int>(type: "int", nullable: false),
                    amount_vnd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    gateway_transaction_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    reconciliation_notes = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    reconciled_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reconciled_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    completed_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOrders", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_PaymentOrders_CreditPackages",
                        column: x => x.package_id,
                        principalSchema: "dbo",
                        principalTable: "CreditPackages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentOrders_ReconciledByUser",
                        column: x => x.reconciled_by_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PaymentOrders_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AnalysisRequests",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cv_file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    job_description_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    analysis_type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    content_hash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    timeout_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    partial_result_expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    error_message = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    completed_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisRequests", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_AnalysisRequests_CVFiles",
                        column: x => x.cv_file_id,
                        principalSchema: "dbo",
                        principalTable: "CVFiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisRequests_JobDescriptions",
                        column: x => x.job_description_id,
                        principalSchema: "dbo",
                        principalTable: "JobDescriptions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AnalysisRequests_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    analysis_request_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    overall_score = table.Column<int>(type: "int", nullable: false),
                    score_label = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    skills_score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    experience_score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    format_score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    education_score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    normalized_denominator = table.Column<int>(type: "int", nullable: false),
                    mandatory_warnings_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    report_details_json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_system_error = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Reports_AnalysisRequests",
                        column: x => x.analysis_request_id,
                        principalSchema: "dbo",
                        principalTable: "AnalysisRequests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptimizedBulletPoints",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    report_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    section_name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    original_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    suggested_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customized_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptimizedBulletPoints", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_OptimizedBulletPoints_Reports",
                        column: x => x.report_id,
                        principalSchema: "dbo",
                        principalTable: "Reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportTickets",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ticket_code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    report_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assigned_to_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    issue_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sla_due_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    resolved_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTickets", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_SupportTickets_AssignedToUser",
                        column: x => x.assigned_to_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SupportTickets_Reports",
                        column: x => x.report_id,
                        principalSchema: "dbo",
                        principalTable: "Reports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SupportTickets_Users",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CompensationRequests",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    proposed_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    approved_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    compensated_credit_type = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    credit_amount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    technical_root_cause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    decision_notes = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    decided_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensationRequests", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CompensationRequests_ApprovedByUser",
                        column: x => x.approved_by_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CompensationRequests_ProposedByUser",
                        column: x => x.proposed_by_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CompensationRequests_Tickets",
                        column: x => x.ticket_id,
                        principalSchema: "dbo",
                        principalTable: "SupportTickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportAccessGrants",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cv_file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    granted_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    granted_to_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    granted_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    revoked_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportAccessGrants", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_SupportAccessGrants_CVFiles",
                        column: x => x.cv_file_id,
                        principalSchema: "dbo",
                        principalTable: "CVFiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportAccessGrants_GrantedByUser",
                        column: x => x.granted_by_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SupportAccessGrants_GrantedToUser",
                        column: x => x.granted_to_user_id,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SupportAccessGrants_Tickets",
                        column: x => x.ticket_id,
                        principalSchema: "dbo",
                        principalTable: "SupportTickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisRequests_cv_file_id",
                schema: "dbo",
                table: "AnalysisRequests",
                column: "cv_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisRequests_dedup",
                schema: "dbo",
                table: "AnalysisRequests",
                columns: new[] { "user_id", "content_hash", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisRequests_job_description_id",
                schema: "dbo",
                table: "AnalysisRequests",
                column: "job_description_id");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisRequests_status_timeout",
                schema: "dbo",
                table: "AnalysisRequests",
                columns: new[] { "status", "timeout_at" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_actor_created",
                schema: "dbo",
                table: "AuditLogs",
                columns: new[] { "actor_user_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_CompensationRequests_approved_by_user_id",
                schema: "dbo",
                table: "CompensationRequests",
                column: "approved_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompensationRequests_proposed_by_user_id",
                schema: "dbo",
                table: "CompensationRequests",
                column: "proposed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_CompensationRequests_ticket",
                schema: "dbo",
                table: "CompensationRequests",
                column: "ticket_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditLedger_user_created",
                schema: "dbo",
                table: "CreditLedger",
                columns: new[] { "user_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_CreditPackages_created_by_user_id",
                schema: "dbo",
                table: "CreditPackages",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CVFiles_user_expires",
                schema: "dbo",
                table: "CVFiles",
                columns: new[] { "user_id", "expires_at" },
                filter: "[is_deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHashRegistry_blocked_until",
                schema: "dbo",
                table: "EmailHashRegistry",
                column: "trial_blocked_until");

            migrationBuilder.CreateIndex(
                name: "IX_JobDescriptions_user_created",
                schema: "dbo",
                table: "JobDescriptions",
                columns: new[] { "user_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_OptimizedBulletPoints_report",
                schema: "dbo",
                table: "OptimizedBulletPoints",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOrders_package_id",
                schema: "dbo",
                table: "PaymentOrders",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOrders_reconciled_by_user_id",
                schema: "dbo",
                table: "PaymentOrders",
                column: "reconciled_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOrders_reconciliation",
                schema: "dbo",
                table: "PaymentOrders",
                columns: new[] { "status", "expires_at" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOrders_user_id",
                schema: "dbo",
                table: "PaymentOrders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_PaymentOrders_code",
                schema: "dbo",
                table: "PaymentOrders",
                column: "order_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_expires",
                schema: "dbo",
                table: "Reports",
                column: "expires_at",
                filter: "[is_system_error] = 0");

            migrationBuilder.CreateIndex(
                name: "UQ_Reports_analysis_request",
                schema: "dbo",
                table: "Reports",
                column: "analysis_request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportAccessGrants_cv_file_id",
                schema: "dbo",
                table: "SupportAccessGrants",
                column: "cv_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAccessGrants_granted_by_user_id",
                schema: "dbo",
                table: "SupportAccessGrants",
                column: "granted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAccessGrants_granted_to_user_id",
                schema: "dbo",
                table: "SupportAccessGrants",
                column: "granted_to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAccessGrants_lookup",
                schema: "dbo",
                table: "SupportAccessGrants",
                columns: new[] { "ticket_id", "granted_to_user_id", "expires_at" },
                filter: "[revoked_at] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_assigned_to_user_id",
                schema: "dbo",
                table: "SupportTickets",
                column: "assigned_to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_report_id",
                schema: "dbo",
                table: "SupportTickets",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_status_sla",
                schema: "dbo",
                table: "SupportTickets",
                columns: new[] { "status", "sla_due_at" });

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_user_id",
                schema: "dbo",
                table: "SupportTickets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_SupportTickets_code",
                schema: "dbo",
                table: "SupportTickets",
                column: "ticket_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_role",
                schema: "dbo",
                table: "Users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_email",
                schema: "dbo",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CompensationRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditLedger",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EmailHashRegistry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OptimizedBulletPoints",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentOrders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SupportAccessGrants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserBalances",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditPackages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SupportTickets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AnalysisRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CVFiles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobDescriptions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
