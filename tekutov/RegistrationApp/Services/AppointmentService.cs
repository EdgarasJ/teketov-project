using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using RegistrationApp.Components.Pages;
using RegistrationApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RegistrationApp.Services
{
    public class AppointmentService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AppointmentService(IDbContextFactory<ApplicationDbContext> contextFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            _contextFactory = contextFactory;
            _authenticationStateProvider = authenticationStateProvider;
        }

        private async Task<uint?> GetLoggedInDoctorIdAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var doctorId = user.FindFirst(c => c.Type == ClaimTypes.PrimarySid)?.Value;
                return uint.TryParse(doctorId, out var id) ? id : null;
            }

            return null;
        }
        public async Task<List<Appointment>> GetAllAppointmentsForDoctorAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var doctorId = await GetLoggedInDoctorIdAsync();
            if (doctorId == null) return new List<Appointment>();

            return await context.Appointments
                .Include(a => a.Timeslot)
                .Include(a => a.Patient)
                    .ThenInclude(p => p.Profile)
                .Where(a => a.doctor_id == doctorId)
                .OrderBy(a => a.Timeslot.time)
                .ToListAsync();
        }

        public async Task<List<Visitfinding>> GetAllVisitFindingsForDoctorAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var doctorId = await GetLoggedInDoctorIdAsync();
            if (doctorId == null) return new List<Visitfinding>();

            return await context.Visitfindings
                .Include(vf => vf.Appointment)
                    .ThenInclude(a => a.Patient)
                        .ThenInclude(p => p.Profile)
                .Include(vf => vf.Appointment)
                    .ThenInclude(a => a.Timeslot)
                .Where(vf => vf.Appointment.doctor_id == doctorId)
                .ToListAsync();
        }
        public async Task<List<Timeslot>> GetAvailableTimeslotsForDoctorAndPatientAsync(uint doctorId, uint patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            var doctorOccupiedTimeslotIds = await context.Appointments
                .Where(a => a.doctor_id == doctorId && (a.status == "Open" || a.status == "Confirmed" || a.status == "Cancelled"))
                .Select(a => a.timeslot_id)
                .ToListAsync();

            var patientOccupiedTimeslotIds = await context.Appointments
                .Where(a => a.patient_id == patientId && (a.status == "Open" || a.status == "Confirmed" || a.status == "Cancelled"))
                .Select(a => a.timeslot_id)
                .ToListAsync();

            var occupiedTimeslotIds = doctorOccupiedTimeslotIds.Union(patientOccupiedTimeslotIds).ToList();

            return await context.Timeslots
                .Where(t => !occupiedTimeslotIds.Contains(t.id))
                .OrderBy(t => t.time)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetConfirmedAppointmentsForDoctorAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var doctorId = await GetLoggedInDoctorIdAsync();
            if (doctorId == null) return new List<Appointment>();

            return await context.Appointments
                .Include(a => a.Timeslot)
                .Include(a => a.Patient)
                    .ThenInclude(p => p.Profile)
                .Where(a => a.status == "Confirmed" && a.doctor_id == doctorId)
                .ToListAsync();
        }
        public async Task<List<Timeslot>> GetAvailableTimeslotsForDoctorAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var doctorId = await GetLoggedInDoctorIdAsync();
            if (doctorId == null) return new List<Timeslot>();

            var occupiedTimeslotIds = await context.Appointments
                .Where(a => a.status == "Open" || a.status == "Confirmed")
                .Select(a => a.timeslot_id)
                .ToListAsync();

            return await context.Timeslots
                .Where(t => !occupiedTimeslotIds.Contains(t.id))
                .OrderBy(t => t.time)
                .ToListAsync();
        }

        public async Task<List<Timeslot>> GetAvailableTimeslotsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Timeslots
                .Where(t => t.availability == "Available")
                .OrderBy(t => t.time)
                .ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Patients
                .Include(p => p.Profile)
                .OrderBy(p => p.Profile.name)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Doctors
                .Include(d => d.User)
                .Include(d => d.Category)
                .OrderBy(d => d.User.id)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(uint appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Appointments
                .Include(a => a.Timeslot)
                .Include(a => a.Patient)
                    .ThenInclude(p => p.Profile)
                .FirstOrDefaultAsync(a => a.id == appointmentId);
        }

        public async Task<Visitfinding> GetVisitFindingByAppointmentIdAsync(uint appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Visitfindings.FirstOrDefaultAsync(vf => vf.appointment_id == appointmentId);
        }

        public async Task UpdateVisitFindingAsync(Visitfinding visitFinding)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Visitfindings.Update(visitFinding);
            await context.SaveChangesAsync();
        }

        public async Task<List<Visitfinding>> GetAllVisitFindingsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Visitfindings
                .Include(vf => vf.Appointment)
                    .ThenInclude(a => a.Patient)
                        .ThenInclude(p => p.Profile)
                .Include(vf => vf.Appointment)
                    .ThenInclude(a => a.Timeslot)
                .ToListAsync();
        }
        public async Task AddOrUpdateVisitFindingAsync(Visitfinding visitFinding)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingFinding = await context.Visitfindings
                .FirstOrDefaultAsync(vf => vf.appointment_id == visitFinding.appointment_id);

            if (existingFinding != null)
            {
                existingFinding.findings = visitFinding.findings;
                existingFinding.prescription = visitFinding.prescription;
                existingFinding.fallowup = visitFinding.fallowup;
                context.Visitfindings.Update(existingFinding);
            }
            else
            {
                await context.Visitfindings.AddAsync(visitFinding);
            }

            await context.SaveChangesAsync();
        }
        public async Task<bool> IsDuplicateAppointmentAsync(uint doctorId, uint timeslotId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Appointments
                .AnyAsync(a => a.doctor_id == doctorId && a.timeslot_id == timeslotId);
        }

        public async Task ConfirmAppointmentAsync(uint appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.status = "Confirmed";
                await context.SaveChangesAsync();
            }
        }

        public async Task CancelAppointmentAsync(uint appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.status = "Cancelled";
                await context.SaveChangesAsync();
            }
        }

        public async Task ReopenAppointmentAsync(uint appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            var appointment = await context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.status = "Open";
                await context.SaveChangesAsync();
            }
        }

        public async Task AddVisitFindingAsync(Visitfinding finding)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Visitfindings.AddAsync(finding);
            await context.SaveChangesAsync();
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
        }
        public async Task CreateAppointmentForLoggedInDoctorAsync(Appointment appointment)
        {
            using var context = _contextFactory.CreateDbContext();
            var doctorId = await GetLoggedInDoctorIdAsync();
            if (doctorId == null) throw new InvalidOperationException("The logged-in user is not a doctor.");

            appointment.doctor_id = doctorId.Value;
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
        }

    }
}